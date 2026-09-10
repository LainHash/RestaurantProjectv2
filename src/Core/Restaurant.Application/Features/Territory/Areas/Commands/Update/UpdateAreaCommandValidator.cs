using FluentValidation;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Update
{
    public class UpdateAreaCommandValidator
        : AbstractValidator<UpdateAreaCommand>
    {
        public UpdateAreaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.BranchId)
                    .NotEmpty().WithMessage("BranchId is required.");

                RuleFor(x => x.Body.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

                RuleFor(x => x.Body.Description)
                    .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

                RuleFor(x => x.Body.DisplayOrder)
                    .GreaterThanOrEqualTo(0).WithMessage("DisplayOrder must be greater than or equal to 0.");
            });
        }
    }
}
