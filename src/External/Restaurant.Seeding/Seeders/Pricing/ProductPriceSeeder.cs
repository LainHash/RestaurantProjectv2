using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Pricing;

namespace Restaurant.Seeding.Seeders.Pricing
{
    internal class ProductPriceSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductPrices.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();

            var productDictionary = products.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<ProductPriceRecord>("ProductPrices");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductPublicId, out var product))
                    throw new Exception($"Product '{record.ProductPublicId}' not found.");

                var price = new ProductPrice(record.UnitPrice, record.Currency)
                    .SetProduct(product.Id);

                context.ProductPrices.Add(price);
            }

            await context.SaveChangesAsync();
        }
    }
}
