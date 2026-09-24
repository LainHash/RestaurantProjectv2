using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Product?> FindProductForOrderAsync(
            Guid id,
            long branchId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> FindProductsForOrderAsync(
            IEnumerable<Guid> productIds,
            long branchId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> ToListPopularProductsAsync(
            int limit = 10,
            CancellationToken cancellationToken = default);
    }
}
