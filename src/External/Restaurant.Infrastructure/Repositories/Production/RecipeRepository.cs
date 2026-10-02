using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Production
{
    internal class RecipeRepository(RestaurantDbContext context)
        : Repository<Recipe>(context), IRecipeRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
