using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Identity
{
    internal class OtpVerificationRepository(RestaurantDbContext context) 
        : Repository<OtpVerification>(context), IOtpVerificationRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<OtpVerification?> FindActiveAsync(long userId, OtpPurpose purpose, CancellationToken cancellationToken = default)
        {
            return await _context.OtpVerifications
                .Where(x => x.UserId == userId &&
                            x.Purpose == purpose &&
                            x.IsAvailable &&
                            x.UsedAt == null &&
                            x.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
