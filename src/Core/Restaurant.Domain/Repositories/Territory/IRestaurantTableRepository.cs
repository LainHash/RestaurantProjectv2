using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Repositories.Territory
{
    public interface IRestaurantTableRepository : IRepository<RestaurantTable>
    {
        Task<bool> IsExistingTableNumberAsync(string tableNumber, CancellationToken cancellationToken = default);
    }
}
