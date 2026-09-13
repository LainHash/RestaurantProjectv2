using MediatR;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(ForgotPasswordRequest Body)
        : IRequest<Result>
    {
    }
}
