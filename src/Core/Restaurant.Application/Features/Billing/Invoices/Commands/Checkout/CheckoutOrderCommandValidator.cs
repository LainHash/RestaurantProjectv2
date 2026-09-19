using FluentValidation;

namespace Restaurant.Application.Features.Billing.Invoices.Commands.Checkout
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.OrderId)
                    .NotEmpty().WithMessage("OrderId is required.");
            });
        }
    }
}
