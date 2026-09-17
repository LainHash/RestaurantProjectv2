using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Identity
{
    internal class PersonalProfileRepository(RestaurantDbContext context)
                : Repository<PersonalProfile>(context), IPersonalProfileRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<PersonalProfile?> FindByUserAsync(long userId, CancellationToken cancellationToken = default)
        {
            return await _context.PersonalProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }
    }
}
