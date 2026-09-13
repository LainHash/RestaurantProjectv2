using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Restaurant.Application.Features.Auth.Commands.ChangePassword;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Logout;
using Restaurant.Application.Features.Auth.Commands.RefreshToken;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Application.Features.Auth.Commands.ResetPassword;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.Settings.Auth;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Infrastructure.Services.Auth
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRefreshTokenRepository _refreshTokenRepository;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IOtpVerificationService _otpVerificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider,
            IMapper mapper,
            ILogger<AuthenticationService> logger,
            ICustomerRepository customerRepository,
            IOtpVerificationService otpVerificationService,
            IUserRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _logger = logger;
            _customerRepository = customerRepository;
            _otpVerificationService = otpVerificationService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<AuthenticationResponse>> LoginAsync(
            LoginCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
            if (user is null || !_passwordHasher.VerifyPassword(command.Body.Password, user.PasswordHash))
            {
                return Result<AuthenticationResponse>
                    .Fail("Incorrect email or password.", HttpStatusCode.Unauthorized);
            }

            if (!user.IsActive)
            {
                return Result<AuthenticationResponse>
                    .Fail("Account is not active. Please verify your email.", HttpStatusCode.PreconditionRequired);
            }

            var role = await _roleRepository.FindByIdAsync(user.RoleId, cancellationToken);
            var roleName = role?.Name ?? "Customer";

            var accessToken = _jwtProvider.GenerateToken(user.PublicId, user.UserName, user.Email, roleName);
            var rawRefreshToken = _jwtProvider.GenerateRefreshToken();
            var tokenHash = _jwtProvider.HashToken(rawRefreshToken);

            var refreshToken = new UserRefreshToken(
                user.Id,
                tokenHash,
                DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays));

            _refreshTokenRepository.Add(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new AuthenticationResponse(user, accessToken, rawRefreshToken);
            return Result<AuthenticationResponse>
                .Succeed(response, "Login successfully.");
        }

        public async Task<Result> RegisterAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingUser = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
                if (existingUser is not null)
                {
                    return Result<object>
                        .Fail("This email already used. Please use another email.", HttpStatusCode.Conflict);
                }

                var customerRole = await _roleRepository.FindByNameAsync("Customer", cancellationToken);
                if (customerRole is null)
                {
                    return Result<object>
                        .Fail(Error<Role>.NotFound, HttpStatusCode.InternalServerError);
                }

                var user = _mapper.Map<User>(command.Body)
                    .SetPasswordHash(_passwordHasher.HashPassword(command.Body.Password))
                    .SetRole(customerRole.Id);
                _userRepository.Add(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _otpVerificationService.InitializeAsync(user, cancellationToken);

                var customer = new Customer(user.Id);
                _customerRepository.Add(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result
                    .Succeed("Register successfully. Please check your account to get verification code.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Register request failed. Email: {Email}", command.Body.Email);
                return Result
                    .Fail("Register request failed.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<AuthenticationResponse>> RefreshTokenAsync(
            RefreshTokenCommand command,
            CancellationToken cancellationToken = default)
        {
            var tokenHash = _jwtProvider.HashToken(command.Body.RefreshToken);
            var storedToken = await _refreshTokenRepository.FindActiveByTokenHashAsync(tokenHash, cancellationToken);

            if (storedToken is null || !storedToken.IsActive)
            {
                return Result<AuthenticationResponse>
                    .Fail("Invalid or expired refresh token.", HttpStatusCode.Unauthorized);
            }

            var user = await _userRepository.FindByIdWithRoleAsync(storedToken.User.PublicId, cancellationToken);
            if (user is null || !user.IsActive)
            {
                return Result<AuthenticationResponse>
                    .Fail("User not found or account is inactive.", HttpStatusCode.Unauthorized);
            }

            var role = await _roleRepository.FindByIdAsync(user.RoleId, cancellationToken);
            var roleName = role?.Name ?? "Customer";

            storedToken.Revoke();

            var newAccessToken = _jwtProvider.GenerateToken(user.PublicId, user.UserName, user.Email, roleName);
            var newRawRefreshToken = _jwtProvider.GenerateRefreshToken();
            var newTokenHash = _jwtProvider.HashToken(newRawRefreshToken);

            var newRefreshToken = new UserRefreshToken(
                storedToken.UserId,
                newTokenHash,
                DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays));

            _refreshTokenRepository.Add(newRefreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new AuthenticationResponse(user, newAccessToken, newRawRefreshToken);
            return Result<AuthenticationResponse>
                .Succeed(response, "Token refreshed successfully.");
        }

        public async Task<Result> LogoutAsync(
            LogoutCommand command,
            CancellationToken cancellationToken = default)
        {
            var tokenHash = _jwtProvider.HashToken(command.RefreshToken);
            var storedToken = await _refreshTokenRepository.FindActiveByTokenHashAsync(tokenHash, cancellationToken);

            if (storedToken is null)
            {
                return Result.Succeed("Logged out successfully.");
            }

            storedToken.Revoke();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Succeed("Logged out successfully.");
        }

        public async Task<Result> LogoutAllAsync(
            LogoutAllCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(command.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Fail("User not found.", HttpStatusCode.NotFound);
            }

            await _refreshTokenRepository.RevokeAllByUserIdAsync(user.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Succeed("All sessions logged out successfully.");
        }

        public async Task<Result> ResetPasswordAsync(
            ResetPasswordCommand command,
            CancellationToken cancellationToken = default)
        {
            if (!_jwtProvider.TryValidatePasswordResetToken(command.Body.ResetToken, out var userId))
            {
                return Result.Fail("Invalid or expired reset token.", HttpStatusCode.Unauthorized);
            }

            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                return Result.Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            var newPasswordHash = _passwordHasher.HashPassword(command.Body.NewPassword);
            user.SetPasswordHash(newPasswordHash);

            await _refreshTokenRepository.RevokeAllByUserIdAsync(user.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Succeed("Password reset successfully. Please login with your new password.");
        }

        public async Task<Result> ChangePasswordAsync(
            ChangePasswordCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(command.UserId, cancellationToken);
            if (user is null || !user.IsActive)
            {
                return Result.Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            if (!_passwordHasher.VerifyPassword(command.Body.OldPassword, user.PasswordHash))
            {
                return Result.Fail("Current password is incorrect.", HttpStatusCode.BadRequest);
            }

            var newPasswordHash = _passwordHasher.HashPassword(command.Body.NewPassword);
            user.SetPasswordHash(newPasswordHash);

            await _refreshTokenRepository.RevokeAllByUserIdAsync(user.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Succeed("Password changed successfully. Please login with your new password.");
        }
    }
}
