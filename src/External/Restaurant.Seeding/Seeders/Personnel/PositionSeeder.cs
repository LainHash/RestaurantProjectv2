using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Seeders.Personnel
{
    internal class PositionSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Positions.AnyAsync())
                return;

            var departments = await context.Departments
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var departmentsDictionary = departments.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<PositionRecord>("Positions");

            foreach (var record in records)
            {
                if (!departmentsDictionary.TryGetValue(record.DepartmentId, out var department))
                    throw new Exception($"Category '{record.DepartmentId}' not found.");

                var position = _mapper.Map<Position>(record)
                    .SetDepartment(department.Id);

                context.Positions.Add(position);
            }

            await context.SaveChangesAsync();
        }
    }
}
