using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Repositories.Territory
{
    public interface IRestaurantTableRepository : IRepository<RestaurantTable>
    {
        Task<bool> IsExistingTableNumberAsync(string tableNumber, CancellationToken cancellationToken = default);
        Task<RestaurantTable?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<RestaurantTable?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
