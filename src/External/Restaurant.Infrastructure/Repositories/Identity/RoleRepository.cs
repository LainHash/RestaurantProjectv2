using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Identity
{
    internal class RoleRepository(RestaurantDbContext context)
        : Repository<Role>(context), IRoleRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Role?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Role?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
    }
}
