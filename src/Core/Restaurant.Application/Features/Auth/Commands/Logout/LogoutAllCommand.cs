using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Logout
{
    /// <summary>
    /// Revokes ALL refresh tokens for the currently authenticated user (logout all devices).
    /// </summary>
    public record LogoutAllCommand(Guid UserId)
        : IRequest<Result>;
}
