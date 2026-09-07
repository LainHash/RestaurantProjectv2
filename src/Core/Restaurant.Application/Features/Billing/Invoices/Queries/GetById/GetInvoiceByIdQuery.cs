using MediatR;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetById
{
    public record GetInvoiceByIdQuery(Guid Id)
        : IRequest<Result<InvoiceResponse>>
    {
    }
}
