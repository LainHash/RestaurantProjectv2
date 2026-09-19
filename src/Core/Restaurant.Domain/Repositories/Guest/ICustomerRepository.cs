using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Repositories.Guest
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> FindByUserIdAsync(long userId, CancellationToken cancellationToken = default);
        Task<Customer?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Customer?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Customer?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Customer?> FindFullPersonalProfileAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Customer?> FindByUserIdWithWalletAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
