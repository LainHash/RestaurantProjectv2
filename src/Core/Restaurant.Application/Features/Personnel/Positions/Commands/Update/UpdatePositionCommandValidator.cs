using FluentValidation;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Update
{
    public class UpdatePositionCommandValidator
        : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator()
        {
            RuleFor(x => x.Body.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

            RuleFor(x => x.Body.DepartmentPublicId)
                .NotEmpty().WithMessage("DepartmentId is required.");

            RuleFor(x => x.Body.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Body.Description));
        }
    }
}
