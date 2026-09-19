using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Application.Features.Sale.Orders.Queries.GetById;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Sale
{
    public interface IOrderService
    {
        Task<PageResult<IEnumerable<OrderResponse>>> GetAllAsync(
            GetAllOrdersSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<OrderResponse>> GetByIdAsync(
            GetOrderByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<OrderResponse>> CreateAsync(
            CreateOrderCommand command,
            CreateOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<OrderResponse>> AddItemsAsync(
            Guid orderId,
            AddOrderItemsRequest request,
            CreateOrderSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
