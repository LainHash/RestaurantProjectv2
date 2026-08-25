using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Inventory
{
    internal class IngredientStockRepository(RestaurantDbContext context)
        : Repository<IngredientStock>(context), IIngredientStockRepository
    {
    }
}
