using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Production;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Production;

namespace Restaurant.Seeding.Seeders.Production
{
    internal class RecipeIngredientSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.RecipeIngredients.AnyAsync())
                return;

            var ingredients = await context.Ingredients
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var ingredientsDictionary = ingredients.ToDictionary(
                x => x.PublicId);

            var recipes = await context.Recipes
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var recipeDictionary = recipes.ToDictionary(
                x => x.PublicId);

            var units = await context.Units
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var unitDictionary = units.ToDictionary(
                x => x.PublicId);


            var records =
                _importer.Read<RecipeIngredientRecord>("RecipeIngredients");

            foreach (var record in records)
            {
                if (!ingredientsDictionary.TryGetValue(record.IngredientPublicId, out var ingredient))
                    throw new Exception($"Ingredient '{record.IngredientPublicId}' not found.");

                if (!recipeDictionary.TryGetValue(record.RecipePublicId, out var recipe))
                    throw new Exception($"Recipe '{record.RecipePublicId}' not found.");

                if (!unitDictionary.TryGetValue(record.UnitPublicId, out var unit))
                    throw new Exception($"Unit '{record.UnitPublicId}' not found.");

                var recipeIngredient = _mapper.Map<RecipeIngredient>(record)
                    .SetIngredient(ingredient.Id)
                    .SetRecipe(recipe.Id)
                    .SetUnit(unit.Id);

                context.RecipeIngredients.Add(recipeIngredient);
            }

            await context.SaveChangesAsync();
        }
    }
}
