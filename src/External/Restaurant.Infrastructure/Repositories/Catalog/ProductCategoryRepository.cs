using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Catalog
{
    internal class ProductCategoryRepository(RestaurantDbContext context)
        : Repository<ProductCategory>(context), IProductCategoryRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<ProductCategory?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<ProductCategory?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<ProductCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
