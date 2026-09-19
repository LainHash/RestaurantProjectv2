using MediatR;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Invoices.Commands.Checkout
{
    internal class CheckoutOrderCommandHandler(IInvoiceService invoiceService)
        : IRequestHandler<CheckoutOrderCommand, Result<InvoiceResponse>>
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        public async Task<Result<InvoiceResponse>> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new CheckoutOrderSpecification();
            var response = await _invoiceService.CheckoutAsync(request.Body, specification, cancellationToken);
            return response;
        }
    }
}
