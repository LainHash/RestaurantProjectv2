using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Repositories.Territory
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<Branch?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Branch?> FindByCodeAsync(string branchCode, CancellationToken cancellationToken = default);
    }
}
