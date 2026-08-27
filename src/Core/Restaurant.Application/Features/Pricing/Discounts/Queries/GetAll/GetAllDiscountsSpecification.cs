using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll
{
    public class GetAllDiscountsSpecification
        : BaseSpecification<Discount>
    {
        public GetAllDiscountsSpecification(GetAllDiscountsQuery query)
        {
            EnableSoftDeleteFilter();
        }
    }
}
