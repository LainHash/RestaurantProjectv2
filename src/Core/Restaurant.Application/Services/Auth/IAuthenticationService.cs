using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Logout;
using Restaurant.Application.Features.Auth.Commands.RefreshToken;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<Result<AuthenticationResponse>> LoginAsync(
            LoginCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> RegisterAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default);

        Task<Result<AuthenticationResponse>> RefreshTokenAsync(
            RefreshTokenCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> LogoutAsync(
            LogoutCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> LogoutAllAsync(
            LogoutAllCommand command,
            CancellationToken cancellationToken = default);
    }
}
