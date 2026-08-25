using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Catalog
{
    internal class ProductRepository(RestaurantDbContext context)
        : Repository<Product>(context), IProductRepository
    {
        private readonly RestaurantDbContext _context = context;
        public async Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }
    }
}
