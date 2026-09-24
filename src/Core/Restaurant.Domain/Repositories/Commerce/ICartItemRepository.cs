using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Domain.Repositories.Commerce
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<int> CountAsync(long cartId, CancellationToken cancellationToken = default);
    }
}
