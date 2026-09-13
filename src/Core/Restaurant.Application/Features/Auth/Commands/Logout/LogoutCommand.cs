using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand(string RefreshToken)
        : IRequest<Result>;
}
