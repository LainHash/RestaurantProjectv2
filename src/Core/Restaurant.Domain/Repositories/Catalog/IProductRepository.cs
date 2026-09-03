using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Product?> FindProductForOrderAsync(
            Guid id,
            int branchId,
            CancellationToken cancellationToken = default);

        Task<List<Product>> FindProductsForOrderAsync(
            IEnumerable<Guid> productIds,
            int branchId,
            CancellationToken cancellationToken = default);
    }
}
