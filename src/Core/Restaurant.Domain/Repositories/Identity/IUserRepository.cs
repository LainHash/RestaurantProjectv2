using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<User?> FindByIdWithRoleAsync(Guid id, CancellationToken cancellationToken = default);

        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
