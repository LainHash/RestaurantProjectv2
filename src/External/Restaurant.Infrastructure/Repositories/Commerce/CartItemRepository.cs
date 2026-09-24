using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Repositories.Commerce
{
    internal class CartItemRepository(RestaurantDbContext context)
        : Repository<CartItem>(context), ICartItemRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<int> CountAsync(long cartId, CancellationToken cancellationToken = default)
        {
            return await _context.CartItems
                .Where(x => x.CartId == cartId)
                .CountAsync(cancellationToken);
        }
    }
}
