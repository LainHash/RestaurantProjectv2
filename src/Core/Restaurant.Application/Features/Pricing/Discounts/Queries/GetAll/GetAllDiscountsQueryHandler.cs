using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll
{
    internal class GetAllDiscountsQueryHandler(IDiscountService discountService)
                : IRequestHandler<GetAllDiscountsQuery, PageResult<IEnumerable<DiscountResponse>>>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<PageResult<IEnumerable<DiscountResponse>>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllDiscountsSpecification(request);
            var response = await _discountService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
