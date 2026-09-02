using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Sale
{
    public interface IOrderService
    {
        Task<PageResult<IEnumerable<OrderResponse>>> GetAllAsync(
            GetAllOrdersSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
