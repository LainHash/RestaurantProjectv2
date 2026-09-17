using Restaurant.Domain.Entities.Storage;

namespace Restaurant.Domain.Repositories.Storage
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<int> CountByProductIdAsync(long productId, CancellationToken cancellationToken = default);

        Task UnsetPrimaryAsync(long productId, CancellationToken cancellationToken = default);
    }
}
