using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, ChangePasswordRequest Body)
        : IRequest<Result>
    {
    }
}
