using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Domain.Repositories.Commerce
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> FindByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default);
        Task<Cart?> FindBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default);
        Task<Cart?> FindWithItemsByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default);
        Task<Cart?> FindWithItemsBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default);
    }
}
