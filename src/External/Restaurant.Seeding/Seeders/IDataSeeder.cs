using Restaurant.Infrastructure.Context;

namespace Restaurant.Seeding.Seeders
{
    internal interface IDataSeeder
    {
        Task SeedAsync(RestaurantDbContext context);
    }
}
