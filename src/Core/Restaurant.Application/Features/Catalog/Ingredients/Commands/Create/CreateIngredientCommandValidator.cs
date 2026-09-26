using FluentValidation;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Create
{
    public class CreateIngredientCommandValidator
        : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(x => x.Body.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Body.CategoryPublicId)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleFor(x => x.Body.UnitPublicId)
                .NotEmpty().WithMessage("UnitId is required.");

            RuleFor(x => x.Body.UnitPrice)
                .GreaterThan(0).WithMessage("UnitPrice must be greater than 0.");

            RuleFor(x => x.Body.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Body.Description));
        }
    }
}
