using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Territory
{
    internal class RestaurantTableRepository(RestaurantDbContext context) 
        : Repository<RestaurantTable>(context), IRestaurantTableRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingTableNumberAsync(string tableNumber, CancellationToken cancellationToken = default)
        {
            return await _context.RestaurantTables.AnyAsync(x => x.TableNumber == tableNumber, cancellationToken);
        }

        public async Task<RestaurantTable?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.RestaurantTables
                .Include(x => x.Area)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<RestaurantTable?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.RestaurantTables
                .Include(x => x.Area)
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }
    }
}
