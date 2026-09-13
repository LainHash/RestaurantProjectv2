using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Auth.Commands.ChangePassword;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Logout;
using Restaurant.Application.Features.Auth.Commands.RefreshToken;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Application.Features.Auth.Commands.ResetPassword;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyPasswordResetOtp;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.Update;
using Restaurant.Application.Features.Identity.Users.Queries.GetById;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        IMediator mediator,
        ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest body,
            CancellationToken cancellationToken)
        {
            var command = new LoginCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest body,
            CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(
            [FromBody] VerifyEmailRequest body,
            CancellationToken cancellationToken)
        {
            var command = new VerifyEmailCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }


        [AllowAnonymous]
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(
            [FromBody] CompleteProfileRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CompleteProfileCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification(
            [FromBody] ResendVerificationRequest body,
            CancellationToken cancellationToken)
        {
            var command = new ResendVerificationCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequest body,
            CancellationToken cancellationToken)
        {
            var command = new RefreshTokenCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            [FromBody] RefreshTokenRequest body,
            CancellationToken cancellationToken)
        {
            var command = new LogoutCommand(body.RefreshToken);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPost("logout-all")]
        public async Task<IActionResult> LogoutAll(
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.PublicId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var command = new LogoutAllCommand(userId.Value);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            [FromBody] ForgotPasswordRequest body,
            CancellationToken cancellationToken)
        {
            var command = new ForgotPasswordCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("verify-reset-otp")]
        public async Task<IActionResult> VerifyResetOtp(
            [FromBody] VerifyPasswordResetOtpRequest body,
            CancellationToken cancellationToken)
        {
            var command = new VerifyPasswordResetOtpCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordRequest body,
            CancellationToken cancellationToken)
        {
            var command = new ResetPasswordCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPost("me/change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordRequest body,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.PublicId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var command = new ChangePasswordCommand(userId.Value, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPut("me/profile")]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdatePersonalProfileRequest body,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.PublicId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var command = new UpdatePersonalProfileCommand(userId.Value, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser(
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.PublicId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var query = new GetUserByIdQuery(userId.Value);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
