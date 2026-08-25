using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

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
