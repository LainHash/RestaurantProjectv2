using MediatR;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetAll
{
    internal class GetAllInvoicesQueryHandler(IInvoiceService invoiceService)
                : IRequestHandler<GetAllInvoicesQuery, PageResult<IEnumerable<InvoiceResponse>>>
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        public async Task<PageResult<IEnumerable<InvoiceResponse>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllInvoicesSpecification(request);
            var response = await _invoiceService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
