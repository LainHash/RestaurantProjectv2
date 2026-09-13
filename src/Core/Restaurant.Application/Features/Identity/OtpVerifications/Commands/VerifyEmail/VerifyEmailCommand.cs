using MediatR;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail
{
    public record VerifyEmailCommand(VerifyEmailRequest Body)
        : IRequest<Result>
    {
    }
}
