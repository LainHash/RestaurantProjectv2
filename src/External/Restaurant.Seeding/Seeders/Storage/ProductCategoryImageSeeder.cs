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

            var products = await context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategory)
                .ToListAsync();

            foreach (var product in products)
            {
                var categoryImage = new ProductCategoryImage()
                    .SetProductCategory(product.CategoryId)
                    .SetImage(product.ProductImages.First().ImageId);

                context.ProductCategoryImages.Add(categoryImage);
            }

            await context.SaveChangesAsync();
        }
    }
}
