using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Create
{
    internal class CreateDiscountCommandHandler(IDiscountService discountService)
                : IRequestHandler<CreateDiscountCommand, Result<DiscountResponse>>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<Result<DiscountResponse>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var response = await _discountService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
