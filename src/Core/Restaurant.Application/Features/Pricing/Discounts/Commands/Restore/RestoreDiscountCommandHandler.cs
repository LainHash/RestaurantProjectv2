using MediatR;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Restore
{
    internal class RestoreDiscountCommandHandler(IDiscountService discountService)
                : IRequestHandler<RestoreDiscountCommand, Result>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<Result> Handle(RestoreDiscountCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreDiscountSpecification(request);
            var response = await _discountService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
