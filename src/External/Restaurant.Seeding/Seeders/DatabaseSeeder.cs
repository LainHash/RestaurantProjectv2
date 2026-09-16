using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.Seeders.Catalog;
using Restaurant.Seeding.Seeders.Guest;
using Restaurant.Seeding.Seeders.Identity;
using Restaurant.Seeding.Seeders.Inventory;
using Restaurant.Seeding.Seeders.Personnel;
using Restaurant.Seeding.Seeders.Pricing;
using Restaurant.Seeding.Seeders.Production;
using Restaurant.Seeding.Seeders.Storage;
using Restaurant.Seeding.Seeders.Territory;

namespace Restaurant.Seeding.Seeders
{
    internal class DatabaseSeeder(
        IServiceProvider serviceProvider,
        RestaurantDbContext context)
    {
        private readonly RestaurantDbContext _context = context;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task SeedAllAsync()
        {
            await SeedAsync<ProductCategorySeeder>(_context);
            await SeedAsync<IngredientCategorySeeder>(_context);

            await SeedAsync<BrandSeeder>(_context);
            await SeedAsync<UnitSeeder>(_context);


            await SeedAsync<ProductSeeder>(_context);
            await SeedAsync<ProductPriceSeeder>(_context);

            await SeedAsync<IngredientSeeder>(_context);
            await SeedAsync<IngredientPriceSeeder>(_context);

            await SeedAsync<BranchSeeder>(_context);
            await SeedAsync<AreaSeeder>(_context);

            await SeedAsync<ProductStockSeeder>(_context);
            await SeedAsync<IngredientStockSeeder>(_context);


            await SeedAsync<ImageSeeder>(_context);
            await SeedAsync<ProductImageSeeder>(_context);
            await SeedAsync<ProductCategoryImageSeeder>(_context);
            await SeedAsync<BrandImageSeeder>(_context);

            await SeedAsync<RecipeSeeder>(_context);
            await SeedAsync<RecipeIngredientSeeder>(_context);

            await SeedAsync<RoleSeeder>(_context);
            await SeedAsync<UserSeeder>(_context);
            await SeedAsync<PersonalProfileSeeder>(_context);

            await SeedAsync<DepartmentSeeder>(_context);
            await SeedAsync<PositionSeeder>(_context);

            await SeedAsync<EmployeeSeeder>(_context);
            await SeedAsync<CustomerSeeder>(_context);

            await SeedAsync<DiscountSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(RestaurantDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
