using MediatR;
using Restaurant.Contract.DTOs.Commerce.CartItems;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.UpdateItemQuantity
{
    public record UpdateCartItemQuantityCommand(Guid? UserId, string? SessionId, UpdateCartItemQuantityRequest Body)
        : IRequest<Result<CartResponse>>
    {
    }
}
