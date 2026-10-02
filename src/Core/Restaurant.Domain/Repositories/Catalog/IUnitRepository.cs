using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
