using FluentValidation;

namespace Restaurant.Application.Features.Sale.Orders.Commands.AddItems
{
    public class AddOrderItemsCommandValidator : AbstractValidator<AddOrderItemsCommand>
    {
        public AddOrderItemsCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.OrderDetails)
                    .NotEmpty().WithMessage("At least one order item is required.");

                RuleFor(x => x.Body.OrderDetails)
                    .Must(x => x.Select(i => i.ProductId).Distinct().Count() == x.Count())
                    .WithMessage("Each product can only appear once in an order update.");

                RuleForEach(x => x.Body.OrderDetails).ChildRules(detail =>
                {
                    detail.RuleFor(d => d.ProductId)
                        .NotEmpty().WithMessage("ProductId is required.");

                    detail.RuleFor(d => d.Quantity)
                        .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

                    detail.RuleFor(d => d.Note)
                        .MaximumLength(1000).WithMessage("Note must not exceed 1000 characters.")
                        .When(d => !string.IsNullOrEmpty(d.Note));
                });
            });
        }
    }
}
