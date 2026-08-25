using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
