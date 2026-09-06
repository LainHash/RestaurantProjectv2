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
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var brandDictionary = brands.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var images = await context.Images
                .Select(x => new { x.Id, x.AltText })
                .ToListAsync();
            var imageDictionary = images.ToDictionary(
                x => x.AltText,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<BrandImageRecord>("BrandImages");

            foreach (var record in records)
            {
                if (!brandDictionary.TryGetValue(record.BrandName.Trim(), out var brand))
                    throw new Exception($"Brand '{record.BrandName}' not found.");

                if (!imageDictionary.TryGetValue(record.AltText.Trim(), out var image))
                    throw new Exception($"Image '{record.AltText}' not found.");

                var brandImage = _mapper.Map<BrandImage>(record)
                    .SetBrand(brand.Id)
                    .SetImage(image.Id);

                context.BrandImages.Add(brandImage);
            }

            await context.SaveChangesAsync();
        }
    }
}
