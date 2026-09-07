using MediatR;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetById
{
    internal class GetInvoiceByIdQueryHandler(IInvoiceService invoiceService)
                : IRequestHandler<GetInvoiceByIdQuery, Result<InvoiceResponse>>
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        public async Task<Result<InvoiceResponse>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetInvoiceByIdSpecification(request);
            var response = await _invoiceService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
