using MediatR;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetByOrderId
{
    internal class GetInvoiceByOrderIdQueryHandler(IInvoiceService invoiceService)
                : IRequestHandler<GetInvoiceByOrderIdQuery, Result<InvoiceResponse>>
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        public async Task<Result<InvoiceResponse>> Handle(GetInvoiceByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetInvoiceByOrderIdSpecification(request);
            var response = await _invoiceService.GetByOrderIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
