using Restaurant.Application.Features.Billing.Invoices.Queries.GetAll;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetById;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Billing
{
    public interface IInvoiceService
    {
        Task<PageResult<IEnumerable<InvoiceResponse>>> GetAllAsync(
            GetAllInvoicesSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<InvoiceResponse>> GetByIdAsync(
            GetInvoiceByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task InitializeAsync(Order order, CancellationToken cancellationToken = default);
    }
}
