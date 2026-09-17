using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Domain.Repositories.Personnel
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Employee?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Employee?> FindByUserIdAsync(long userId, CancellationToken cancellationToken = default);
        Task<Employee?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
