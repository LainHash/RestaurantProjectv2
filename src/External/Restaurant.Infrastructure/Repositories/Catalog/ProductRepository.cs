using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;
using Restaurant.Domain.Enums;

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

        public async Task<Product?> FindProductForOrderAsync(
            Guid id,
            int branchId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.ProductPrice)
                .Include(x => x.ProductStocks)
                .Where(x => x.ProductStocks.Any(s => s.BranchId == branchId))
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<List<Product>> FindProductsForOrderAsync(
            IEnumerable<Guid> productIds,
            int branchId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.ProductPrice)
                .Include(x => x.ProductStocks
                    .Where(s => s.BranchId == branchId))
                .Where(x =>
                    productIds.Contains(x.PublicId) &&
                    (
                        (x.InventoryType == InventoryType.StockTracked &&
                         x.ProductStocks.Any(s => s.BranchId == branchId))
                        ||
                        x.InventoryType == InventoryType.MadeToOrder
                    ))
                .ToListAsync(cancellationToken);
        }
    }
}
