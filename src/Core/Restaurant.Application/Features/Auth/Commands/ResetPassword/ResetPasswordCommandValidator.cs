using FluentValidation;

namespace Restaurant.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator
        : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Body.ResetToken)
                .NotEmpty().WithMessage("Reset token is required.");

            RuleFor(x => x.Body.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters.");

            RuleFor(x => x.Body.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Body.NewPassword).WithMessage("Confirm password does not match new password.");
        }
    }
}
