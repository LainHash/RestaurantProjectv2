using FluentValidation;

namespace Restaurant.Application.Features.Billing.Payments.Commands.CreateUrl
{
    public class CreateVnPayPaymentUrlCommandValidator : AbstractValidator<CreateVnPayPaymentUrlCommand>
    {
        public CreateVnPayPaymentUrlCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull()
                .WithMessage("Payment request body is required.");

            RuleFor(x => x.Body.InvoiceId)
                .NotEmpty()
                .WithMessage("InvoiceId is required.");
        }
    }
}
