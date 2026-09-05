using AutoMapper;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetAll;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetById;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Billing;
using System.Net;

namespace Restaurant.Infrastructure.Services.Billing
{
    internal class InvoiceService(
        IInvoiceRepository invoiceRepository,
        IMapper mapper) : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PageResult<IEnumerable<InvoiceResponse>>> GetAllAsync(
            GetAllInvoicesSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _invoiceRepository.CountAsync(specification, cancellationToken);

            var invoices = await _invoiceRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<InvoiceResponse>>(invoices);
            return PageResult<IEnumerable<InvoiceResponse>>
                .Succeed(response, Success<Invoice>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<InvoiceResponse>> GetByIdAsync(
            GetInvoiceByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var invoice = await _invoiceRepository.FindAsync(specification, cancellationToken);
            if (invoice is null)
            {
                return Result<InvoiceResponse>
                    .Fail(Error<Invoice>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<InvoiceResponse>(invoice);
            return Result<InvoiceResponse>
                .Succeed(response, Success<Invoice>.Retrieved);
        }
    }
}
