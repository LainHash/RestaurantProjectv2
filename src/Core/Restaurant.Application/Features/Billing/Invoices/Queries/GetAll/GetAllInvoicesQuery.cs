using MediatR;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetAll
{
    public record GetAllInvoicesQuery(
        string? OrderCode)
        : PageQuery, IRequest<PageResult<IEnumerable<InvoiceResponse>>>
    {
    }
}
