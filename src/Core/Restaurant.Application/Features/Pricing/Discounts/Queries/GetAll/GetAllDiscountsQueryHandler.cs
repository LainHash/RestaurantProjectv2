using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll
{
    internal class GetAllDiscountsQueryHandler(IDiscountService discountService)
                : IRequestHandler<GetAllDiscountsQuery, Result<IEnumerable<DiscountResponse>>>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<Result<IEnumerable<DiscountResponse>>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllDiscountsSpecification(request);
            var response = await _discountService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
