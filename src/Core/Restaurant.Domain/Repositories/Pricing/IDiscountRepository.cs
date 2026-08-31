using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Domain.Repositories.Pricing
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<int> ReserveRemainingQuantityAsync(int discountId, CancellationToken cancellationToken = default);
    }
}
