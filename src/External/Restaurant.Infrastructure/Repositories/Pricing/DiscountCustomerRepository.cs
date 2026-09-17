using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Repositories.Pricing;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Pricing
{
    internal class DiscountCustomerRepository(RestaurantDbContext context) 
        : Repository<DiscountCustomer>(context), IDiscountCustomerRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<DiscountCustomer?> FindByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.DiscountsCustomer.FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
