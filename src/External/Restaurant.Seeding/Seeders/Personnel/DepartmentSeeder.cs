using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Seeders.Personnel
{
    internal class DepartmentSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Departments.AnyAsync())
                return;

            var records =
                _importer.Read<DepartmentRecord>("Departments");

            var entities =
                _mapper.Map<List<Department>>(records);

            context.Departments.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
