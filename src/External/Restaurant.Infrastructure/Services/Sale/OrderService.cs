using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Application.Features.Sale.Orders.Queries.GetById;
using Restaurant.Application.Services.Billing;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Inventory;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.OrderDetails;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Guest;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        private readonly IInventoryDeductionService _inventoryDeductionService;

        private readonly IInvoiceService _invoiceService;

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
            IRestaurantTableRepository restaurantTableRepository,
            IInventoryDeductionService inventoryDeductionService,
            ILogger<OrderService> logger,
            IInvoiceService invoiceService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _restaurantTableRepository = restaurantTableRepository;
            _inventoryDeductionService = inventoryDeductionService;
            _logger = logger;
            _invoiceService = invoiceService;
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
                // --- Resolve Customer (optional for DineIn/TakeAway, required for Delivery) ---
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

                // --- Resolve Branch ---
                var branch = await _branchRepository
                    .FindByIdAsync(command.Body.BranchId, cancellationToken);
                if (branch is null)
                {
                    return Result<OrderResponse>
                        .Fail(Error.NotFound("Branch"), HttpStatusCode.NotFound);
                }

                // --- Type-specific: Employee & Table ---
                long? employeeId = null;
                long? restaurantTableId = null;

                if (command.Body.Type != OrderType.Delivery)
                {
                    // Employee required for DineIn / TakeAway
                    var employee = await _employeeRepository
                        .FindByIdAsync(command.Body.EmployeeId!.Value, cancellationToken);
                    if (employee is null)
                    {
                        return Result<OrderResponse>
                            .Fail(Error.NotFound("Employee"), HttpStatusCode.NotFound);
                    }

                    employeeId = employee.Id;
                }

                if (command.Body.Type == OrderType.DineIn)
                {
                    var tableResult = await ResolveAndOccupyTableAsync(
                        command.Body.RestaurantTableId!.Value,
                        branch.Id,
                        cancellationToken);

                    if (!tableResult.IsSucceed)
                    {
                        return Result<OrderResponse>
                            .Fail(tableResult.Message, (HttpStatusCode)tableResult.StatusCode);
                    }

                    restaurantTableId = tableResult.Data!.Id;
                }

                // --- Build Order ---
                var order = Order.Create(
                    customer?.Id,
                    employeeId,
                    branch.Id,
                    restaurantTableId,
                    command.Body.Type,
                    command.Body.Note,
                    command.Body.DeliveryAddress);

                // --- Process Products, OrderDetails & Reserve Inventory ---
                var orderDetailsResult = await ProcessOrderDetailsAndInventoryAsync(
                    order,
                    branch.Id,
                    command.Body.CreateOrderDetails,
                    cancellationToken);

                if (!orderDetailsResult.IsSucceed)
                {
                    return Result<OrderResponse>
                        .Fail(orderDetailsResult.Message, (HttpStatusCode)orderDetailsResult.StatusCode);
                }

                _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // --- Create Invoice only for Delivery orders ---
                if (command.Body.Type == OrderType.Delivery)
                {
                    await _invoiceService.InitializeAsync(order, cancellationToken);
                }

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

        private async Task<Result<RestaurantTable>> ResolveAndOccupyTableAsync(
            Guid tableId,
            long branchId,
            CancellationToken cancellationToken)
        {
            var table = await _restaurantTableRepository
                .FindByIdAsync(tableId, cancellationToken);

            if (table is null)
            {
                return Result<RestaurantTable>
                    .Fail(Error.NotFound("RestaurantTable"), HttpStatusCode.NotFound);
            }

            if (table.Area is not null && table.Area.BranchId != branchId)
            {
                return Result<RestaurantTable>
                    .Fail("Restaurant table does not belong to the selected branch.", HttpStatusCode.BadRequest);
            }

            if (table.Status != TableStatus.Available)
            {
                return Result<RestaurantTable>
                    .Fail(Error.Occupied("RestaurantTable"), HttpStatusCode.Conflict);
            }

            table.Occupy();
            return Result<RestaurantTable>.Succeed(table, "Restaurant table occupied successfully.");
        }

        private async Task<Result> ProcessOrderDetailsAndInventoryAsync(
            Order order,
            long branchId,
            IEnumerable<CreateOrderDetailRequest> items,
            CancellationToken cancellationToken)
        {
            var detailsResult = await CreateOrderDetailsAsync(order, branchId, items, cancellationToken);
            if (!detailsResult.IsSucceed)
            {
                return Result.Fail(detailsResult.Message, (HttpStatusCode)detailsResult.StatusCode);
            }

            var reserveResult = _inventoryDeductionService
                .ReserveInventoryForOrder(branchId, detailsResult.Data!, cancellationToken);

            if (!reserveResult.IsSucceed)
            {
                return Result.Fail(reserveResult.Message, (HttpStatusCode)reserveResult.StatusCode);
            }

            order.CalculateTotalAmount();
            return Result.Succeed("Order details and inventory processed successfully.");
        }

        private async Task<Result<List<(Product Product, int Quantity)>>> CreateOrderDetailsAsync(
            Order order,
            long branchId,
            IEnumerable<CreateOrderDetailRequest> items,
            CancellationToken cancellationToken)
        {
            var productIds = items
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            var products = await _productRepository
                .FindProductsForOrderAsync(
                    productIds,
                    branchId,
                    cancellationToken);

            var productMap = products.ToDictionary(x => x.PublicId);
            var orderItems = new List<(Product Product, int Quantity)>();

            foreach (var item in items)
            {
                if (!productMap.TryGetValue(item.ProductId, out var product))
                {
                    return Result<List<(Product Product, int Quantity)>>
                        .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
                }

                orderItems.Add((product, item.Quantity));

                var orderDetail = new OrderDetail(item.Quantity, item.Note)
                    .SetProduct(product)
                    .CalculateLineTotal();

                order.AddOrderDetail(orderDetail);
            }

            return Result<List<(Product Product, int Quantity)>>
                .Succeed(orderItems, "Order details created successfully.");
        }
    }
}
