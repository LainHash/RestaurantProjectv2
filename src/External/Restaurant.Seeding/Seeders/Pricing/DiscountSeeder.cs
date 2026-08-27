using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Catalog;
using Restaurant.Seeding.DataRecords.Pricing;

namespace Restaurant.Seeding.Seeders.Pricing
{
    internal class DiscountSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Discounts.AnyAsync())
                return;

            var records =
                _importer.Read<DiscountRecord>("Discounts");

            var entities =
                _mapper.Map<List<Discount>>(records);

            context.Discounts.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
