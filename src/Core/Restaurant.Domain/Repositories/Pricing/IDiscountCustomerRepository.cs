using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Domain.Repositories.Pricing
{
    public interface IDiscountCustomerRepository : IRepository<DiscountCustomer>
    {
        Task<DiscountCustomer?> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    }
}
