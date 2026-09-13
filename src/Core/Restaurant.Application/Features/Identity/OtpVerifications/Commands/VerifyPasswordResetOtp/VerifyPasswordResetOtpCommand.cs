using MediatR;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyPasswordResetOtp
{
    public record VerifyPasswordResetOtpCommand(string Email, VerifyPasswordResetOtpRequest Body)
        : IRequest<Result<VerifyPasswordResetOtpResponse>>
    {
    }
}
