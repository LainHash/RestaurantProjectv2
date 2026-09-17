using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Repositories.Territory
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<Area?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Area?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsExistingNameAsync(long branchId, string name, CancellationToken cancellationToken = default);
    }
}
