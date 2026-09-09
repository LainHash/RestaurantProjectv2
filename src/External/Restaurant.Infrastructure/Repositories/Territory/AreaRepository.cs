using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Territory
{
    internal class AreaRepository(RestaurantDbContext context) 
        : Repository<Area>(context), IAreaRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
