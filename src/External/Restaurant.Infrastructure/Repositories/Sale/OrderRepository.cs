using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Sale
{
    internal class OrderRepository(RestaurantDbContext context)
        : Repository<Order>(context), IOrderRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
