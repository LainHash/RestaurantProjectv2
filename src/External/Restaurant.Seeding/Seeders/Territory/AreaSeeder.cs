using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Territory;

namespace Restaurant.Seeding.Seeders.Territory
{
    internal class AreaSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Areas.AnyAsync())
                return;

            var branches = await context.Branches
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var branchDictionary = branches.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<AreaRecord>("Areas");

            foreach (var record in records)
            {
                if (!branchDictionary.TryGetValue(record.BranchPublicId, out var branch))
                    throw new Exception($"Branch '{record.BranchPublicId}' not found.");

                var area = _mapper.Map<Area>(record)
                    .SetBranch(branch.Id);

                context.Areas.Add(area);
            }

            await context.SaveChangesAsync();
        }
    }
}
