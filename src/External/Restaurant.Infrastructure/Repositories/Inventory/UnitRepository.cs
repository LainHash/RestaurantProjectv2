using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;

namespace Restaurant.Infrastructure.Repositories.Inventory
{
    internal class UnitRepository(RestaurantDbContext context)
        : Repository<Unit>(context), IUnitRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Unit?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Units.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }
    }
}
