using MediatR;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Commands.CreateFromCart
{
    public record CreateOrderFromCartCommand(
        Guid? UserId,
        string? SessionId,
        CreateOrderFromCartRequest Body
    ) : IRequest<Result<OrderResponse>>;
}
