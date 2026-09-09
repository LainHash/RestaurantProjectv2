using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Territory
{
    internal class RestaurantTableRepository(RestaurantDbContext context) 
        : Repository<RestaurantTable>(context), IRestaurantTableRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
