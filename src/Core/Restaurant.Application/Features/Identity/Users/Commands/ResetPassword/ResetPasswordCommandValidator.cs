using FluentValidation;

namespace Restaurant.Application.Features.Identity.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator
        : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Body.NewPassword)
                .NotEmpty().WithMessage("New Password is required.")
                .MinimumLength(6).WithMessage("New Password must be at least 6 characters.");

            RuleFor(x => x.Body.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm New Password is required.")
                .Equal(x => x.Body.NewPassword).WithMessage("Confirm New Password must match the Password.");
        }
    }
}
