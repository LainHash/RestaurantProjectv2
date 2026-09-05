using Restaurant.Domain.Entities.Sale;

namespace Restaurant.Domain.Repositories.Sale
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> FindWithOrderDetailAsync(int id, CancellationToken cancellationToken = default);
    }
}
