using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Production
{
    internal class RecipeIngredientRepository(RestaurantDbContext context) 
        : Repository<RecipeIngredient>(context), IRecipeIngredientRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
