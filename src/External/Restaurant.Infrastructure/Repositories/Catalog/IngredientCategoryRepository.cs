using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;

namespace Restaurant.Infrastructure.Repositories.Catalog
{
    internal class IngredientCategoryRepository(RestaurantDbContext context)
        : Repository<IngredientCategory>(context), IIngredientCategoryRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<IngredientCategory?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IngredientCategory?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<IngredientCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
