using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Production;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Production;

namespace Restaurant.Seeding.Seeders.Production
{
    internal class RecipeSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Recipes.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var productDictionary = products.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<RecipeRecord>("Recipes");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductPublicId, out var product))
                    throw new Exception($"Product '{record.ProductPublicId}' not found.");

                var recipe = _mapper.Map<Recipe>(record)
                    .SetProduct(product.Id);

                context.Recipes.Add(recipe);
            }

            await context.SaveChangesAsync();
        }
    }
}
