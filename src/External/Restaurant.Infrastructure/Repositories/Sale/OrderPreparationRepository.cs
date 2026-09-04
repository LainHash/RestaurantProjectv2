using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Sale
{
    internal class OrderPreparationRepository(RestaurantDbContext context)
        : Repository<OrderPreparation>(context), IOrderPreparationRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
