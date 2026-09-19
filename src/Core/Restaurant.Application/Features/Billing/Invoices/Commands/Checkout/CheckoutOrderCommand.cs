using MediatR;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Commands.Checkout
{
    public record CheckoutOrderCommand(CheckoutOrderRequest Body)
        : IRequest<Result<InvoiceResponse>>
    {
    }
}
