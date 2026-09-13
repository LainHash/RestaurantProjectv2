using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordRequest Body)
        : IRequest<Result>
    {
    }
}
