using AutoMapper;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetAll;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetById;
using Restaurant.Application.Services.Billing;
using Restaurant.Application.Services.Business;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Billing;
using System.Net;

namespace Restaurant.Infrastructure.Services.Billing
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<InvoiceResponse>>> GetAllAsync(
            GetAllInvoicesSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _invoiceRepository.CountAsync(specification, cancellationToken);

            var invoices = await _invoiceRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<InvoiceResponse>>(invoices);
            return PageResult<IEnumerable<InvoiceResponse>>
                .Succeed(response, Success.Retrieved("Invoice"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<InvoiceResponse>> GetByIdAsync(
            GetInvoiceByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var invoice = await _invoiceRepository.FindAsync(specification, cancellationToken);
            if (invoice is null)
            {
                return Result<InvoiceResponse>
                    .Fail(Error.NotFound("Invoice"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<InvoiceResponse>(invoice);
            return Result<InvoiceResponse>
                .Succeed(response, Success.Retrieved("Invoice"));
        }

        public async Task InitializeAsync(Order order, CancellationToken cancellationToken = default)
        {
            var invoice = new Invoice(order);
            _invoiceRepository.Add(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
