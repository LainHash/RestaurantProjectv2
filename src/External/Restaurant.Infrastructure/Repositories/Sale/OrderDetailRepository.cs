using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Sale
{
    internal class OrderDetailRepository(RestaurantDbContext context) 
        : Repository<OrderDetail>(context), IOrderDetailRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
