using MediatR;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Claim
{
    internal class ClaimDiscountCommandHandler(IDiscountService discountService)
                : IRequestHandler<ClaimDiscountCommand, Result>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<Result> Handle(ClaimDiscountCommand request, CancellationToken cancellationToken)
        {
            var response = await _discountService.ClaimAsync(request, cancellationToken);
            return response;
        }
    }
}
