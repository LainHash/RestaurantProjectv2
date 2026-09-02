using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Delete
{
    public class DeleteDiscountSpecification
        : BaseSpecification<Discount>
    {
        public DeleteDiscountSpecification(DeleteDiscountCommand command)
        {
            Criteria = discount => discount.PublicId == command.Id;
        }
    }
}
