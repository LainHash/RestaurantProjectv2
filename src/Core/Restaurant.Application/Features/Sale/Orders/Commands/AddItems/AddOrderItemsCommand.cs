using MediatR;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Commands.AddItems
{
    public record AddOrderItemsCommand(Guid OrderId, AddOrderItemsRequest Body)
        : IRequest<Result<OrderResponse>>
    {
    }
}
