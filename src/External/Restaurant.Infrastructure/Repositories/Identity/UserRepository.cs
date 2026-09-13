using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Infrastructure.Context;
using Restaurant.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Infrastructure.Repositories.Identity
{
    internal class UserRepository(RestaurantDbContext context) 
        : Repository<User>(context), IUserRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);
        }

        public async Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<User?> FindByIdWithRoleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }
    }
}
