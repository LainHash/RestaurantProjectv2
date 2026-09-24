using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> ToListWithImagesAsync(CancellationToken cancellationToken = default);

        Task<ProductCategory?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<ProductCategory?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProductCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
