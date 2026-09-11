using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Seeders.Personnel
{
    internal class EmployeeSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Employees.AnyAsync())
                return;

            var users = await context.Users
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var usersDictionary = users.ToDictionary(
                x => x.PublicId);

            var positions = await context.Positions
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var positionsDictionary = positions.ToDictionary(
                x => x.PublicId);

            var branches = await context.Branches
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var branchDictionary = branches.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<EmployeeRecord>("Employees");

            foreach (var record in records)
            {
                if (!usersDictionary.TryGetValue(record.UserPublicId, out var user))
                    throw new Exception($"User '{record.UserPublicId}' not found.");

                if (!positionsDictionary.TryGetValue(record.PositionPublicId, out var position))
                    throw new Exception($"Position '{record.PositionPublicId}' not found.");

                if (!branchDictionary.TryGetValue(record.BranchPublicId, out var branch))
                    throw new Exception($"Branch '{record.BranchPublicId}' not found.");

                var employee = _mapper.Map<Employee>(record)
                    .SetUser(user.Id)
                    .SetPosition(position.Id)
                    .SetBranch(branch.Id);

                context.Employees.Add(employee);
            }

            await context.SaveChangesAsync();
        }
    }
}
