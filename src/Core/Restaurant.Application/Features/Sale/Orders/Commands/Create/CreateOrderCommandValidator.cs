using FluentValidation;

namespace Restaurant.Application.Features.Sale.Orders.Commands.Create
{
    public class CreateOrderCommandValidator
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.EmployeeId)
                    .NotEmpty().WithMessage("EmployeeId is required.");

                RuleFor(x => x.Body.BranchId)
                    .NotEmpty().WithMessage("BranchId is required.");

                RuleFor(x => x.Body.CustomerId)
                    .Must(id => id != Guid.Empty)
                    .When(x => x.Body.CustomerId.HasValue)
                    .WithMessage("CustomerId must not be empty.");

                RuleFor(x => x.Body.Type)
                    .IsInEnum().WithMessage("Invalid order type.");

                RuleFor(x => x.Body.Note)
                    .MaximumLength(1000).WithMessage("Note must not exceed 1000 characters.")
                    .When(x => !string.IsNullOrEmpty(x.Body.Note));

                RuleFor(x => x.Body.CreateOrderDetails)
                    .NotEmpty().WithMessage("Order must contain at least one order detail.");

                RuleFor(x => x.Body.CreateOrderDetails)
                    .Must(x => x.Select(i => i.ProductId).Distinct().Count() == x.Count())
                    .WithMessage("Each product can only appear once in an order.");

                RuleForEach(x => x.Body.CreateOrderDetails).ChildRules(detail =>
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
