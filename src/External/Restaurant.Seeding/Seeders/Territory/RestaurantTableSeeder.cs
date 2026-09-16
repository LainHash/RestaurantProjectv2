using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Territory;

namespace Restaurant.Seeding.Seeders.Territory
{
    internal class RestaurantTableSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.RestaurantTables.AnyAsync())
                return;

            var records =
                _importer.Read<RestaurantTableRecord>("RestaurantTables");

            var areas = await context.Areas
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var areaDictionary = areas.ToDictionary(
                x => x.PublicId);

            foreach (var record in records)
            {
                if (!areaDictionary.TryGetValue(record.AreaPublicId, out var area))
                    throw new Exception($"Area '{record.AreaPublicId}' not found.");

                var restaurantTable = _mapper.Map<RestaurantTable>(record)
                    .SetArea(area.Id);

                context.RestaurantTables.Add(restaurantTable);
            }

            await context.SaveChangesAsync();
        }
    }
}
