using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Repositories.Commerce
{
    internal class WishlistItemRepository(RestaurantDbContext context)
        : Repository<WishlistItem>(context), IWishlistItemRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<int> CountAsync(long wishlistId, CancellationToken cancellationToken = default)
        {
            return await _context.WishlistItems
                .Where(x => x.WishlistId == wishlistId)
                .CountAsync(cancellationToken);
        }
    }
}
