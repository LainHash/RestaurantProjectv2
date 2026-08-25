using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Commerce
{
    internal class CartItemRepository(RestaurantDbContext context) 
        : Repository<CartItem>(context), ICartItemRepository
    {
    }
}
