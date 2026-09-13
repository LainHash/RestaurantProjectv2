using FluentValidation;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator
        : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            // Email is extracted from the authenticated user's JWT — no validation needed.
        }
    }
}
