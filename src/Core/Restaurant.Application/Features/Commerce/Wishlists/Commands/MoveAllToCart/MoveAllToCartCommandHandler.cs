using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.MoveAllToCart
{
    internal class MoveAllToCartCommandHandler(IWishlistService wishlistService)
        : IRequestHandler<MoveAllToCartCommand, Result<CartResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<CartResponse>> Handle(
            MoveAllToCartCommand request,
            CancellationToken cancellationToken)
        {
            var specification = new MoveAllToCartSpecification(request);
            var response = await _wishlistService.MoveAllToCartAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
