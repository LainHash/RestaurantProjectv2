using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Repositories.Production;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Production
{
    internal class RecipeIngredientRepository(RestaurantDbContext context) 
        : Repository<RecipeIngredient>(context), IRecipeIngredientRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
