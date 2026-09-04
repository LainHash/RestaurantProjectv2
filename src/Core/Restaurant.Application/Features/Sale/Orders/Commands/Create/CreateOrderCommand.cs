using MediatR;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Commands.Create
{
    public record CreateOrderCommand(CreateOrderRequest Body)
        : IRequest<Result<OrderResponse>>
    {
    }
}
