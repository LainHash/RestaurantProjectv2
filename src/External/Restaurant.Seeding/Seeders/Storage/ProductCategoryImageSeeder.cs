using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Storage;

namespace Restaurant.Seeding.Seeders.Storage
{
    internal class ProductCategoryImageSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductCategoryImages.AnyAsync())
                return;

            var categories = await context.ProductCategories
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var categoryDictionary = categories.ToDictionary(
                x => x.PublicId);

            var images = await context.Images
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var imageDictionary = images.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<ProductCategoryImageRecord>("ProductCategoryImages");

            foreach (var record in records)
            {
                if (!categoryDictionary.TryGetValue(record.CategoryId, out var category))
                    throw new Exception($"Category '{record.CategoryId}' not found.");

                if (!imageDictionary.TryGetValue(record.ImageId, out var image))
                    throw new Exception($"Image '{record.ImageId}' not found.");

                var categoryImage = _mapper.Map<ProductCategoryImage>(record)
                    .SetProductCategory(category.Id)
                    .SetImage(image.Id);

                context.ProductCategoryImages.Add(categoryImage);
            }

            await context.SaveChangesAsync();
        }
    }
}
