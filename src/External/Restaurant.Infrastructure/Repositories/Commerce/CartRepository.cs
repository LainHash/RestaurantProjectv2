using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Commerce
{
    internal class CartRepository(RestaurantDbContext context)
        : Repository<Cart>(context), ICartRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Cart?> FindByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }

        public async Task<Cart?> FindBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.SessionId == sessionId, cancellationToken);
        }

        public async Task<Cart?> FindWithItemsByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }

        public async Task<Cart?> FindWithItemsBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.SessionId == sessionId, cancellationToken);
        }
    }
}
