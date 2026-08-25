using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Domain.Repositories.Personnel
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Employee?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Employee?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<Employee?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
