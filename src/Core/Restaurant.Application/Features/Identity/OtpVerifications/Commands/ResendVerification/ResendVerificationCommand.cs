using MediatR;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification
{
    public record ResendVerificationCommand(ResendVerificationRequest Body)
        : IRequest<Result>
    {
    }
}
