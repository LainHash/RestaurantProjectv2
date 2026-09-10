using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Repositories.Territory
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<Area?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Area?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsExistingNameAsync(int branchId, string name, CancellationToken cancellationToken = default);
    }
}
