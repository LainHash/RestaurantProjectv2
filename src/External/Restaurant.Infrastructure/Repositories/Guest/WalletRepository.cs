using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Guest
{
    internal class WalletRepository(RestaurantDbContext context) 
        : Repository<Wallet>(context), IWalletRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Wallet?> FindByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Wallets.FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
