using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Repositories.Production;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Production
{
    internal class RecipeRepository(RestaurantDbContext context)
        : Repository<Recipe>(context), IRecipeRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
