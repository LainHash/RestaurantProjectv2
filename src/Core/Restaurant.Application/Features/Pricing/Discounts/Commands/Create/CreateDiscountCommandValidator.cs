using FluentValidation;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Create
{
    public class CreateDiscountCommandValidator
        : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Body.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Body.Value)
                .GreaterThan(0).WithMessage("Value must be greater than 0.");

            RuleFor(x => x.Body.TotalQuantity)
                .GreaterThan(0).WithMessage("TotalQuantity must be greater than 0.");

            RuleFor(x => x.Body.StartAt)
                .NotEmpty().WithMessage("StartAt is required.");

            RuleFor(x => x.Body.EndAt)
                .NotEmpty().WithMessage("EndAt is required.")
                .GreaterThan(x => x.Body.StartAt).WithMessage("EndAt must be after StartAt.");

            RuleFor(x => x.Body.MaximumDiscountAmount)
                .GreaterThan(0).WithMessage("MaximumDiscountAmount must be greater than 0.")
                .When(x => x.Body.MaximumDiscountAmount.HasValue);

            RuleFor(x => x.Body.MinimumOrderAmount)
                .GreaterThanOrEqualTo(0).WithMessage("MinimumOrderAmount must be greater than or equal to 0.")
                .When(x => x.Body.MinimumOrderAmount.HasValue);
        }
    }
}
