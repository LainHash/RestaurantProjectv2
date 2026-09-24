using Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.MoveAllToCart;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Commerce
{
    public interface IWishlistService
    {
        Task<Result<WishlistResponse>> GetAsync(
            GetWishlistQuery query,
            GetWishlistSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<WishlistResponse>> AddItemAsync(
            AddWishlistItemCommand command,
            AddWishlistItemSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<WishlistResponse>> RemoveItemAsync(
            RemoveWishlistItemCommand command,
            RemoveWishlistItemSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<CartResponse>> MoveAllToCartAsync(
            MoveAllToCartCommand command,
            MoveAllToCartSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
