using MediatR;
using Restaurant.Contract.DTOs.Commerce.CartItems;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.AddItem
{
    public record AddCartItemCommand(Guid? UserId, string? SessionId, AddCartItemRequest Body)
        : IRequest<Result<CartResponse>>
    {
    }
}
