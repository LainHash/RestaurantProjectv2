using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Inventory;

namespace Restaurant.Seeding.Seeders.Inventory
{
    internal class ProductStockSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductStocks.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var productDictionary = products.ToDictionary(
                x => x.PublicId);

            var branches = await context.Branches
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var branchDictionary = branches.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<ProductStockRecord>("ProductStocks");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductId, out var product))
                    throw new Exception($"Product '{record.ProductId}' not found.");

                if (!branchDictionary.TryGetValue(record.BranchId, out var branch))
                    throw new Exception($"Branch '{record.BranchId}' not found.");

                var productStock = _mapper.Map<ProductStock>(record)
                    .SetProduct(product.Id)
                    .SetBranch(branch.Id);

                context.ProductStocks.Add(productStock);
            }

            await context.SaveChangesAsync();
        }
    }
}
