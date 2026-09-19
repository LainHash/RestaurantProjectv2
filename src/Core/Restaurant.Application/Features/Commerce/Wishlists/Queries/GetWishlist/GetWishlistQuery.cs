using MediatR;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist
{
    public record GetWishlistQuery(Guid? UserId, string? SessionId)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
