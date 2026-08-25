using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Inventory
{
    internal class ProductStockRepository(RestaurantDbContext context) 
        : Repository<ProductStock>(context), IProductStockRepository
    {
    }
}
