using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Storage;

namespace Restaurant.Seeding.Seeders.Storage
{
    internal class ProductImageSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductImages.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var productDictionary = products.ToDictionary(
                x => x.PublicId);

            var images = await context.Images
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var imageDictionary = images.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<ProductImageRecord>("ProductImages");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductId, out var product))
                    throw new Exception($"Product '{record.ProductId}' not found.");

                if (!imageDictionary.TryGetValue(record.ImageId, out var image))
                    throw new Exception($"Image '{record.ImageId}' not found.");

                var productImage = _mapper.Map<ProductImage>(record)
                    .SetProduct(product.Id)
                    .SetImage(image.Id);

                context.ProductImages.Add(productImage);
            }

            await context.SaveChangesAsync();
        }
    }
}
