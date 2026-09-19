using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.UpdateItemQuantity
{
    internal class UpdateCartItemQuantityCommandHandler(ICartService cartService)
                : IRequestHandler<UpdateCartItemQuantityCommand, Result<CartResponse>>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<Result<CartResponse>> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateCartItemQuantitySpecification(request);
            var response = await _cartService.UpdateItemAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
