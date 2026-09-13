using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Logout;
using Restaurant.Application.Features.Auth.Commands.RefreshToken;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Application.Features.Auth.Commands.ResetPassword;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyPasswordResetOtp;
using Restaurant.Application.Features.Identity.Users.Queries.GetById;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
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

        [Authorize]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            var command = new ForgotPasswordCommand(email);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPost("verify-reset-otp")]
        public async Task<IActionResult> VerifyResetOtp(
            [FromBody] VerifyPasswordResetOtpRequest body,
            CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            var command = new VerifyPasswordResetOtpCommand(email, body);
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
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser(
            CancellationToken cancellationToken)
        {
            Guid userId = Guid.Empty;

            if (User.Identity?.IsAuthenticated == true)
            {
                Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out userId);
            }

            var query = new GetUserByIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
