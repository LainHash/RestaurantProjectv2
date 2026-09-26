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
using Restaurant.Application.Features.Sale.Orders.Commands.CreateFromCart;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Commerce;
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
        private readonly ICartRepository _cartRepository;

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
            IInvoiceService invoiceService,
            ICartRepository cartRepository)
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
            _cartRepository = cartRepository;
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
                if (command.Body.CustomerPublicId is not null)
                {
                    customer = await _customerRepository
                        .FindByIdAsync(command.Body.CustomerPublicId.Value, cancellationToken);

                    if (customer is null)
                    {
                        return Result<OrderResponse>
                            .Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
                    }
                }

                var branch = await _branchRepository
                    .FindByIdAsync(command.Body.BranchPublicId, cancellationToken);
                if (branch is null)
                {
                    return Result<OrderResponse>
                        .Fail(Error.NotFound("Branch"), HttpStatusCode.NotFound);
                }

                long? employeeId = null;
                long? restaurantTableId = null;

                if (command.Body.Type != OrderType.Delivery)
                {
                    var employee = await _employeeRepository
                        .FindByIdAsync(command.Body.EmployeePublicId!.Value, cancellationToken);
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
                        command.Body.RestaurantTablePublicId!.Value,
                        branch.Id,
                        cancellationToken);

                    if (!tableResult.IsSucceed)
                    {
                        return Result<OrderResponse>
                            .Fail(tableResult.Message, (HttpStatusCode)tableResult.StatusCode);
                    }

                    restaurantTableId = tableResult.Data!.Id;
                }

                var order = Order.Create(
                    customer?.Id,
                    employeeId,
                    branch.Id,
                    restaurantTableId,
                    command.Body.Type,
                    command.Body.Note,
                    command.Body.DeliveryAddress);

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

        public async Task<Result<OrderResponse>> CreateFromCartAsync(
            CreateOrderFromCartCommand command,
            CreateOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                Customer? customer = null;
                Cart? cart = null;

                if (command.UserId.HasValue)
                {
                    customer = await _customerRepository.FindByUserIdAsync(command.UserId.Value, cancellationToken);
                    if (customer is null)
                    {
                        return Result<OrderResponse>.Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
                    }

                    cart = await _cartRepository.FindWithItemsByCustomerIdAsync(customer.Id, cancellationToken);
                }
                else if (!string.IsNullOrWhiteSpace(command.SessionId))
                {
                    cart = await _cartRepository.FindWithItemsBySessionIdAsync(command.SessionId, cancellationToken);
                }

                if (cart is null || cart.CartItems.Count == 0)
                {
                    return Result<OrderResponse>.Fail("Cart is empty.", HttpStatusCode.BadRequest);
                }

                var branch = await _branchRepository.FindByIdAsync(command.Body.BranchPublicId, cancellationToken);
                if (branch is null)
                {
                    return Result<OrderResponse>.Fail(Error.NotFound("Branch"), HttpStatusCode.NotFound);
                }

                var cartItemsToOrder = cart.CartItems.AsEnumerable();
                if (command.Body.SelectedCartItemIds != null && command.Body.SelectedCartItemIds.Any())
                {
                    var selectedIds = command.Body.SelectedCartItemIds.ToHashSet();
                    cartItemsToOrder = cart.CartItems.Where(x => selectedIds.Contains(x.PublicId)).ToList();

                    if (!cartItemsToOrder.Any())
                    {
                        return Result<OrderResponse>.Fail("No valid cart items found to order.", HttpStatusCode.BadRequest);
                    }
                }

                var orderDetailRequests = cartItemsToOrder.Select(ci => new CreateOrderDetailRequest
                {
                    ProductPublicId = ci.Product.PublicId,
                    Quantity = ci.Quantity
                }).ToList();

                var order = Order.Create(
                    customer?.Id,
                    employeeId: null,
                    branch.Id,
                    restaurantTableId: null,
                    OrderType.Delivery,
                    command.Body.Note,
                    command.Body.DeliveryAddress);

                var orderDetailsResult = await ProcessOrderDetailsAndInventoryAsync(
                    order,
                    branch.Id,
                    orderDetailRequests,
                    cancellationToken);

                if (!orderDetailsResult.IsSucceed)
                {
                    return Result<OrderResponse>
                        .Fail(orderDetailsResult.Message, (HttpStatusCode)orderDetailsResult.StatusCode);
                }

                _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _invoiceService.InitializeAsync(order, cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                specification.ApplyCriteria(order.Id);
                var createdOrder = await _orderRepository.FindAsync(specification, cancellationToken);

                var response = _mapper.Map<OrderResponse>(createdOrder);
                return Result<OrderResponse>
                    .Succeed(response, Success.Created("Order"), HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order from cart.");
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Result<OrderResponse>> AddItemsAsync(
            Guid orderId,
            AddOrderItemsRequest request,
            CreateOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var order = await _orderRepository
                    .FindWithOrderDetailAsync(orderId, cancellationToken);

                if (order is null)
                {
                    return Result<OrderResponse>
                        .Fail(Error.NotFound("Order"), HttpStatusCode.NotFound);
                }

                if (order.Status == OrderStatus.Cancelled)
                {
                    return Result<OrderResponse>
                        .Fail("Cannot add items to a cancelled order.", HttpStatusCode.BadRequest);
                }

                if (order.Status == OrderStatus.Completed)
                {
                    return Result<OrderResponse>
                        .Fail("Cannot add items to a completed order.", HttpStatusCode.BadRequest);
                }

                if (order.Invoice is not null && order.Invoice.Status == InvoiceStatus.Paid)
                {
                    return Result<OrderResponse>
                        .Fail("Cannot add items to an already paid order.", HttpStatusCode.BadRequest);
                }

                var orderDetailsResult = await ProcessOrderDetailsAndInventoryAsync(
                    order,
                    order.BranchId,
                    request.OrderDetails,
                    cancellationToken);

                if (!orderDetailsResult.IsSucceed)
                {
                    return Result<OrderResponse>
                        .Fail(orderDetailsResult.Message, (HttpStatusCode)orderDetailsResult.StatusCode);
                }

                if (order.Status == OrderStatus.Served)
                {
                    order.Prepare();
                }

                if (order.Invoice is not null && order.Invoice.Status == InvoiceStatus.Draft)
                {
                    order.Invoice.UpdateAmounts(order.Subtotal, order.DiscountAmount, order.TaxAmount, order.TotalAmount);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                specification.ApplyCriteria(order.Id);
                var updatedOrder = await _orderRepository.FindAsync(specification, cancellationToken);

                var response = _mapper.Map<OrderResponse>(updatedOrder);
                return Result<OrderResponse>
                    .Succeed(response, "Order items added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add items to order.");

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
                .Select(x => x.ProductPublicId)
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
                if (!productMap.TryGetValue(item.ProductPublicId, out var product))
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
