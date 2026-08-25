using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Commerce
{
    internal class WishlistItemRepository(RestaurantDbContext context)
        : Repository<WishlistItem>(context), IWishlistItemRepository
    {
    }
}
