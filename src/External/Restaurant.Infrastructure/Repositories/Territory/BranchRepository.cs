using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Territory
{
    internal class BranchRepository(RestaurantDbContext context)
        : Repository<Branch>(context), IBranchRepository
    {
        private readonly RestaurantDbContext _context = context;
        public async Task<Branch?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Branches.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Branch?> FindByCodeAsync(string branchCode, CancellationToken cancellationToken = default)
        {
            return await _context.Branches.FirstOrDefaultAsync(x => x.BranchCode == branchCode, cancellationToken);
        }
    }
}
