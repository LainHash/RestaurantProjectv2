using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Infrastructure.Context;
using Restaurant.Infrastructure.Repositories;

namespace Restaurant.Infrastructure.Repositories.Identity
{
    internal class UserRefreshTokenRepository(RestaurantDbContext context)
        : Repository<UserRefreshToken>(context), IUserRefreshTokenRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<UserRefreshToken?> FindActiveByTokenHashAsync(
            string tokenHash,
            CancellationToken cancellationToken = default)
        {
            return await _context.UserRefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(
                    t => t.TokenHash == tokenHash && !t.IsRevoked && t.ExpiresAt > DateTime.UtcNow,
                    cancellationToken);
        }

        public async Task RevokeAllByUserIdAsync(
            long userId,
            CancellationToken cancellationToken = default)
        {
            await _context.UserRefreshTokens
                .Where(t => t.UserId == userId && !t.IsRevoked)
                .ExecuteUpdateAsync(
                    setter => setter.SetProperty(t => t.IsRevoked, true),
                    cancellationToken);
        }
    }
}
