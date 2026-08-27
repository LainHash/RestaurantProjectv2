using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Repositories.Pricing;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Pricing
{
    internal class DiscountRepository(RestaurantDbContext context)
        : Repository<Discount>(context), IDiscountRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
