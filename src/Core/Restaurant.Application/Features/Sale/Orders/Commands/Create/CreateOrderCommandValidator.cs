using FluentValidation;
using Restaurant.Domain.Enums;

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
                // BranchId — always required
                RuleFor(x => x.Body.BranchPublicId)
                    .NotEmpty().WithMessage("BranchId is required.");

                // Type — always required
                RuleFor(x => x.Body.Type)
                    .IsInEnum().WithMessage("Invalid order type.");

                // EmployeeId — required for DineIn and TakeAway, not for Delivery
                RuleFor(x => x.Body.EmployeePublicId)
                    .NotEmpty().WithMessage("EmployeeId is required for dine-in and take-away orders.")
                    .When(x => x.Body.Type != OrderType.Delivery);

                // RestaurantTableId — required only for DineIn
                RuleFor(x => x.Body.RestaurantTablePublicId)
                    .NotEmpty().WithMessage("RestaurantTableId is required for dine-in orders.")
                    .When(x => x.Body.Type == OrderType.DineIn);

                // CustomerId — required for Delivery, optional otherwise (validate non-empty if provided)
                RuleFor(x => x.Body.CustomerPublicId)
                    .NotEmpty().WithMessage("CustomerId is required for delivery orders.")
                    .When(x => x.Body.Type == OrderType.Delivery);

                RuleFor(x => x.Body.CustomerPublicId)
                    .Must(id => id != Guid.Empty)
                    .When(x => x.Body.Type != OrderType.Delivery && x.Body.CustomerPublicId.HasValue)
                    .WithMessage("CustomerId must not be empty.");

                // DeliveryAddress — required for Delivery
                RuleFor(x => x.Body.DeliveryAddress)
                    .NotEmpty().WithMessage("DeliveryAddress is required for delivery orders.")
                    .MaximumLength(500).WithMessage("DeliveryAddress must not exceed 500 characters.")
                    .When(x => x.Body.Type == OrderType.Delivery);

                // Note — optional, max length
                RuleFor(x => x.Body.Note)
                    .MaximumLength(1000).WithMessage("Note must not exceed 1000 characters.")
                    .When(x => !string.IsNullOrEmpty(x.Body.Note));

                // Order details — always required
                RuleFor(x => x.Body.CreateOrderDetails)
                    .NotEmpty().WithMessage("Order must contain at least one order detail.");

                RuleFor(x => x.Body.CreateOrderDetails)
                    .Must(x => x.Select(i => i.ProductPublicId).Distinct().Count() == x.Count())
                    .WithMessage("Each product can only appear once in an order.");

                RuleForEach(x => x.Body.CreateOrderDetails).ChildRules(detail =>
                {
                    detail.RuleFor(d => d.ProductPublicId)
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
