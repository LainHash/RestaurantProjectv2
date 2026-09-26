using FluentValidation;

namespace Restaurant.Application.Features.Sale.Orders.Commands.CreateFromCart
{
    public class CreateOrderFromCartCommandValidator
        : AbstractValidator<CreateOrderFromCartCommand>
    {
        public CreateOrderFromCartCommandValidator()
        {
            RuleFor(x => x)
                .Must(x => x.UserId.HasValue || !string.IsNullOrWhiteSpace(x.SessionId))
                .WithMessage("Either UserId or SessionId is required.");

            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.BranchPublicId)
                    .NotEmpty().WithMessage("BranchId is required.");

                RuleFor(x => x.Body.DeliveryAddress)
                    .NotEmpty().WithMessage("DeliveryAddress is required for online delivery orders.")
                    .MaximumLength(500).WithMessage("DeliveryAddress must not exceed 500 characters.");

                RuleFor(x => x.Body.Note)
                    .MaximumLength(1000).WithMessage("Note must not exceed 1000 characters.")
                    .When(x => !string.IsNullOrEmpty(x.Body.Note));
            });
        }
    }
}
