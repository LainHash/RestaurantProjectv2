using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Inventory;

namespace Restaurant.Seeding.Seeders.Inventory
{
    internal class IngredientStockSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.IngredientStocks.AnyAsync())
                return;

            var ingredients = await context.Ingredients
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();

            var ingredientDictionary = ingredients.ToDictionary(
                x => x.PublicId);

            var branches = await context.Branches
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();

            var branchDictionary = branches.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<IngredientStockRecord>("IngredientStocks");

            foreach (var record in records)
            {
                if (!ingredientDictionary.TryGetValue(record.IngredientId, out var ingredient))
                    throw new Exception($"Ingredient '{record.IngredientId}' not found.");

                if (!branchDictionary.TryGetValue(record.BranchId, out var branch))
                    throw new Exception($"Branch '{record.BranchId}' not found.");

                var ingredientStock = _mapper.Map<IngredientStock>(record)
                    .SetIngredient(ingredient.Id)
                    .SetBranch(branch.Id);

                context.IngredientStocks.Add(ingredientStock);
            }

            await context.SaveChangesAsync();
        }
    }
}
