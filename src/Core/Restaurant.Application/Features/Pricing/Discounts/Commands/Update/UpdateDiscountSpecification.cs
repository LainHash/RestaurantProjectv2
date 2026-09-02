using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Update
{
    public class UpdateDiscountSpecification
        : BaseSpecification<Discount>
    {
        public UpdateDiscountSpecification(UpdateDiscountCommand command)
        {
            Criteria = d => d.PublicId == command.Id;
        }
    }
}
