using MediatR;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetByOrderId
{
    public record GetInvoiceByOrderIdQuery(Guid OrderId)
        : IRequest<Result<InvoiceResponse>>
    {
    }
}
