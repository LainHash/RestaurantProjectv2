using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Territory
{
    internal class AreaRepository(RestaurantDbContext context) 
        : Repository<Area>(context), IAreaRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Area?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Areas.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Area?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Areas.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<bool> IsExistingNameAsync(int branchId, string name, CancellationToken cancellationToken = default)
        {
            return await _context.Areas.AnyAsync(x => x.BranchId == branchId && x.Name == name, cancellationToken);
        }
    }
}
