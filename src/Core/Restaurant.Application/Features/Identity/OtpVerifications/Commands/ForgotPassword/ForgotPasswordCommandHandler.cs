using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword
{
    internal class ForgotPasswordCommandHandler(IOtpVerificationService otpVerificationService)
                : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IOtpVerificationService _otpVerificationService = otpVerificationService;

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _otpVerificationService.SendPasswordResetOtpAsync(request, cancellationToken);
            return response;
        }
    }
}
