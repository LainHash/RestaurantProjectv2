using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Domain.Repositories.Inventory
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
