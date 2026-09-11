using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Storage;

namespace Restaurant.Seeding.Seeders.Storage
{
    internal class BrandImageSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.BrandImages.AnyAsync())
                return;

            var brands = await context.Brands
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var brandDictionary = brands.ToDictionary(
                x => x.PublicId);

            var images = await context.Images
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var imageDictionary = images.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<BrandImageRecord>("BrandImages");

            foreach (var record in records)
            {
                if (!brandDictionary.TryGetValue(record.BrandPublicId, out var brand))
                    throw new Exception($"Brand '{record.BrandPublicId}' not found.");

                if (!imageDictionary.TryGetValue(record.ImagePublicId, out var image))
                    throw new Exception($"Image '{record.ImagePublicId}' not found.");

                var brandImage = new BrandImage()
                    .SetBrand(brand.Id)
                    .SetImage(image.Id);

                context.BrandImages.Add(brandImage);
            }

            await context.SaveChangesAsync();
        }
    }
}
