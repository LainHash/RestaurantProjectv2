using MediatR;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Commands.ResetPassword
{
    public record ResetPasswordCommand(Guid UserId, ResetPasswordRequest Body)
        : IRequest<Result>
    {
    }
}
