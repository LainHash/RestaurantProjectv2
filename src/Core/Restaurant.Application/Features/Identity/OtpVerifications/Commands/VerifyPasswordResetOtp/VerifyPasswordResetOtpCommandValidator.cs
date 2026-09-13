using FluentValidation;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyPasswordResetOtp
{
    public class VerifyPasswordResetOtpCommandValidator
        : AbstractValidator<VerifyPasswordResetOtpCommand>
    {
        public VerifyPasswordResetOtpCommandValidator()
        {
            RuleFor(x => x.Body.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Body.Code)
                .NotEmpty().WithMessage("OTP Code is required.")
                .Length(6).WithMessage("OTP Code must be exactly 6 digits.");
        }
    }
}
