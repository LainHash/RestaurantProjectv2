using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Catalog;

namespace Restaurant.Seeding.Seeders.Catalog
{
    internal class IngredientCategorySeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.IngredientCategories.AnyAsync())
                return;

            var records =
                _importer.Read<IngredientCategoryRecord>("IngredientCategories");

            var entities =
                _mapper.Map<List<IngredientCategory>>(records);

            context.IngredientCategories.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
