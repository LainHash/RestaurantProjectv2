using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyResetPassword
{
    public record VerifyResetPasswordCommand(Guid UserId, string Code)
        : IRequest<Result>
    {
    }
}
