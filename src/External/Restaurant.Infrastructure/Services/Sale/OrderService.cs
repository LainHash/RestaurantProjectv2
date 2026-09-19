using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Application.Features.Sale.Orders.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Inventory;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Infrastructure.Services.Sale
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryDeductionService _inventoryDeductionService;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            IBranchRepository branchRepository,
            IProductRepository productRepository,
            IOrderDetailRepository orderDetailRepository,
            IInventoryDeductionService inventoryDeductionService,
            ILogger<OrderService> logger,
            IInvoiceRepository invoiceRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _inventoryDeductionService = inventoryDeductionService;
            _logger = logger;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<PageResult<IEnumerable<OrderResponse>>> GetAllAsync(
            GetAllOrdersSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _orderRepository.CountAsync(specification, cancellationToken);

            var orders = await _orderRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return PageResult<IEnumerable<OrderResponse>>
                .Succeed(response, Success.Retrieved("Order"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<OrderResponse>> GetByIdAsync(
            GetOrderByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.FindAsync(specification, cancellationToken);
            if (order is null)
            {
                return Result<OrderResponse>
                    .Fail(Error.NotFound("Order"), HttpStatusCode.NotFound);

            }

            var response = _mapper.Map<OrderResponse>(order);
            return Result<OrderResponse>
                .Succeed(response, Success.Retrieved("Order"));
        }

        public async Task<Result<OrderResponse>> CreateAsync(
            CreateOrderCommand command,
            CreateOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                Customer? customer = null;

                if (command.Body.CustomerId is not null)
                {
                    customer = await _customerRepository
                        .FindByIdAsync(command.Body.CustomerId.Value, cancellationToken);

                    if (customer is null)
                    {
                        return Result<OrderResponse>
                            .Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
                    }
                }

                var employee = await _employeeRepository
                    .FindByIdAsync(command.Body.EmployeeId, cancellationToken);
                if (employee is null)
                {
                    return Result<OrderResponse>
                        .Fail(Error.NotFound("Employee"), HttpStatusCode.NotFound);
                }

                var branch = await _branchRepository
                    .FindByIdAsync(command.Body.BranchId, cancellationToken);
                if (branch is null)
                {
                    return Result<OrderResponse>
                        .Fail(Error.NotFound("Branch"), HttpStatusCode.NotFound);
                }

                var order = Order.Create(
                    customer?.Id,
                    employee.Id,
                    branch.Id,
                    command.Body.Type,
                    command.Body.Note);

                var productIds = command.Body.CreateOrderDetails
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList();

                var products = await _productRepository
                    .FindProductsForOrderAsync(
                        productIds,
                        branch.Id,
                        cancellationToken);

                var productMap = products.ToDictionary(x => x.PublicId);
                var orderItems = new List<(Product Product, int Quantity)>();

                foreach (var item in command.Body.CreateOrderDetails)
                {
                    if (!productMap.TryGetValue(item.ProductId, out var product))
                    {
                        return Result<OrderResponse>
                            .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
                    }

                    orderItems.Add((product, item.Quantity));

                    var orderDetail = new OrderDetail(item.Quantity, item.Note)
                        .SetProduct(product)
                        .CalculateLineTotal();

                    order.AddOrderDetail(orderDetail);
                }

                var deductionResult = _inventoryDeductionService
                    .DeductInventoryForOrder(branch.Id, orderItems, cancellationToken);

                if (!deductionResult.IsSucceed)
                {
                    return Result<OrderResponse>
                        .Fail(deductionResult.Message, (HttpStatusCode)deductionResult.StatusCode);
                }

                order.CalculateTotalAmount();

                _orderRepository.Add(order);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var invoice = new Invoice(order);

                _invoiceRepository.Add(invoice);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                specification.ApplyCriteria(order.Id);
                var createdOrder = await _orderRepository.FindAsync(specification, cancellationToken);

                var response = _mapper.Map<OrderResponse>(createdOrder);
                return Result<OrderResponse>
                    .Succeed(response, Success.Created("Order"), HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order.");

                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
