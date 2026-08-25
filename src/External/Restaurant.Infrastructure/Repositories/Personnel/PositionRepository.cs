using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Personnel
{
    internal class PositionRepository(RestaurantDbContext context)
        : Repository<Position>(context), IPositionRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Position?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Positions.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Positions.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
