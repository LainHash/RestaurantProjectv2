using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Domain.Repositories.Commerce
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<Wishlist?> FindByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default);
        Task<Wishlist?> FindBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default);
    }
}
