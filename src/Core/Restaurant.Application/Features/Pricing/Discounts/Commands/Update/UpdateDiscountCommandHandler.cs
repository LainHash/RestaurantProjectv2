using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Update
{
    internal class UpdateDiscountCommandHandler(IDiscountService discountService)
                : IRequestHandler<UpdateDiscountCommand, Result<DiscountResponse>>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<Result<DiscountResponse>> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateDiscountSpecification(request);
            var response = await _discountService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
