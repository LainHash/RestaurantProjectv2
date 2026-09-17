using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Domain.Repositories.Personnel
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Department?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Department?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
