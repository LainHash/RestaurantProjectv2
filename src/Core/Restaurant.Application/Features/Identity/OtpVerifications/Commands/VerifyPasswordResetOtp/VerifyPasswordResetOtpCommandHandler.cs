using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.OtpVerifications;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyPasswordResetOtp
{
    internal class VerifyPasswordResetOtpCommandHandler(IOtpVerificationService otpVerificationService)
                : IRequestHandler<VerifyPasswordResetOtpCommand, Result<VerifyPasswordResetOtpResponse>>
    {
        private readonly IOtpVerificationService _otpVerificationService = otpVerificationService;

        public async Task<Result<VerifyPasswordResetOtpResponse>> Handle(VerifyPasswordResetOtpCommand request, CancellationToken cancellationToken)
        {
            var response = await _otpVerificationService.VerifyPasswordResetOtpAsync(request, cancellationToken);
            return response;
        }
    }
}
