using FluentValidation;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator
        : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Body.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
