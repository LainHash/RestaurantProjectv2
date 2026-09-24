using MediatR;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.MoveAllToCart
{
    public record MoveAllToCartCommand(Guid? UserId, string? SessionId)
        : IRequest<Result<CartResponse>>;
}
