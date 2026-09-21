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
            long branchId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.ProductPrice)
                .Include(x => x.ProductStocks)
                .Where(x => x.ProductStocks.Any(s => s.BranchId == branchId))
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<IEnumerable<Product>> FindProductsForOrderAsync(
            IEnumerable<Guid> productIds,
            long branchId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.ProductPrice)
                .Include(x => x.ProductStocks
                    .Where(s => s.BranchId == branchId))
                .Include(x => x.Recipes)
                    .ThenInclude(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Unit)
                .Include(x => x.Recipes)
                    .ThenInclude(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                            .ThenInclude(i => i.BaseUnit)
                .Include(x => x.Recipes)
                    .ThenInclude(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                            .ThenInclude(i => i.IngredientStocks
                                .Where(s => s.BranchId == branchId))
                .Where(x => productIds.Contains(x.PublicId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> ToListPopularProductsAsync(
            int limit = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.ProductPrice)
                .Include(p => p.ProductCategory)
                .Include(p => p.Brand)
                .Include(p => p.Unit)
                .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                .Include(p => p.OrderDetails)
                    .ThenInclude(od => od.Order)
                .OrderByDescending(p => p.OrderDetails
                                        .Where(od => od.Order.Status == OrderStatus.Completed)
                                        .Sum(od => od.Quantity))
                .Take(limit)
                .ToListAsync(cancellationToken);
        }
    }
}
