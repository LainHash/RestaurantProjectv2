using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Restore
{
    public class RestoreDiscountSpecification
        : BaseSpecification<Discount>
    {
        public RestoreDiscountSpecification(RestoreDiscountCommand command)
        {
            Criteria = discount => discount.PublicId == command.Id;
        }
    }
}
