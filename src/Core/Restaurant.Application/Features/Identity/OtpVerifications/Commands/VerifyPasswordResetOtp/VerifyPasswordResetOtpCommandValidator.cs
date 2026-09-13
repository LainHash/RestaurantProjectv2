using FluentValidation;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyPasswordResetOtp
{
    public class VerifyPasswordResetOtpCommandValidator
        : AbstractValidator<VerifyPasswordResetOtpCommand>
    {
        public VerifyPasswordResetOtpCommandValidator()
        {
            // Email is extracted from the authenticated user's JWT — no validation needed.

            RuleFor(x => x.Body.Code)
                .NotEmpty().WithMessage("OTP Code is required.")
                .Length(6).WithMessage("OTP Code must be exactly 6 digits.");
        }
    }
}
