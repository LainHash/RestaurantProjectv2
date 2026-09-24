using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Domain.Repositories.Commerce
{
    public interface IWishlistItemRepository : IRepository<WishlistItem>
    {
        Task<int> CountAsync(long wishlistId, CancellationToken cancellationToken = default);
    }
}
