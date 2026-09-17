using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Role?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
