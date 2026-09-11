using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Catalog;

namespace Restaurant.Seeding.Seeders.Catalog
{
    internal class IngredientSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Ingredients.AnyAsync())
                return;

            var categories = await context.IngredientCategories
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var categoriesDictionary = categories.ToDictionary(
                x => x.PublicId);

            var brands = await context.Brands
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var brandsDictionary = brands.ToDictionary(
                x => x.PublicId);

            var units = await context.Units
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var unitsDictionary = units.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<IngredientRecord>("Ingredients");


            foreach (var record in records)
            {
                if (!categoriesDictionary.TryGetValue(record.IngredientCategoryPublicId, out var category))
                    throw new Exception($"Category '{record.IngredientCategoryPublicId}' not found.");

                if (!unitsDictionary.TryGetValue(record.UnitPublicId, out var unit))
                    throw new Exception($"Unit '{record.UnitPublicId}' not found.");


                if (!brandsDictionary.TryGetValue(record.BrandPublicId, out var brand))
                    throw new Exception($"Unit '{record.BrandPublicId}' not found.");

                var ingredient = _mapper.Map<Ingredient>(record)
                    .SetCategory(category.Id)
                    .SetBrand(brand?.Id)
                    .SetUnit(unit.Id);

                context.Ingredients.Add(ingredient);
            }

            await context.SaveChangesAsync();
        }
    }
}
