using AutoMapper;
using Restaurant.Application.Features.Billing.Invoices.Commands.Checkout;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetAll;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetById;
using Restaurant.Application.Services.Billing;
using Restaurant.Application.Services.Business;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Domain.Repositories.Sale;
using System.Net;

namespace Restaurant.Infrastructure.Services.Billing
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
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

        public async Task<Result<InvoiceResponse>> CheckoutAsync(
            CheckoutOrderRequest request,
            CheckoutOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository
                .FindWithOrderDetailAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Result<InvoiceResponse>
                    .Fail(Error.NotFound("Order"), HttpStatusCode.NotFound);
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                return Result<InvoiceResponse>
                    .Fail("Cannot checkout a cancelled order.", HttpStatusCode.BadRequest);
            }

            if (order.Status == OrderStatus.Completed)
            {
                return Result<InvoiceResponse>
                    .Fail("Order is already completed.", HttpStatusCode.BadRequest);
            }

            var hasInvoice = await _invoiceRepository
                .HasInvoiceForOrderAsync(order.Id, cancellationToken);

            if (hasInvoice || order.Invoice is not null)
            {
                return Result<InvoiceResponse>
                    .Fail("Invoice for this order has already been created.", HttpStatusCode.Conflict);
            }

            if (order.OrderDetails.Count == 0)
            {
                return Result<InvoiceResponse>
                    .Fail("Order contains no items.", HttpStatusCode.BadRequest);
            }

            var invoice = new Invoice(order);
            invoice.Issue();

            _invoiceRepository.Add(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(invoice.Id);
            var createdInvoice = await _invoiceRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<InvoiceResponse>(createdInvoice);
            return Result<InvoiceResponse>
                .Succeed(response, Success.Created("Invoice"), HttpStatusCode.Created);
        }
    }
}
