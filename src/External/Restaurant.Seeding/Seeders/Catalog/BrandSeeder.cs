using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Catalog;

namespace Restaurant.Seeding.Seeders.Catalog
{
    internal class BrandSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Brands.AnyAsync())
                return;

            var records =
                _importer.Read<BrandRecord>("Brands");

            var entities =
                _mapper.Map<List<Brand>>(records);

            context.Brands.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
