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
                .Select(x => new { x.Id, x.UserName })
                .ToListAsync();
            var usersDictionary = users.ToDictionary(
                x => x.UserName,
                StringComparer.OrdinalIgnoreCase);

            var positions = await context.Positions
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var positionsDictionary = positions.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var branches = await context.Branches
                .Select(x => new { x.Id, x.Code })
                .ToListAsync();
            var branchDictionary = branches.ToDictionary(
                x => x.Code,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<EmployeeRecord>("Employees");

            foreach (var record in records)
            {
                if (!usersDictionary.TryGetValue(record.UserName.ToLower(), out var user))
                    throw new Exception($"User '{record.UserName}' not found.");

                if (!positionsDictionary.TryGetValue(record.PositionName.ToLower(), out var position))
                    throw new Exception($"Position '{record.PositionName}' not found.");

                if (!branchDictionary.TryGetValue(record.BranchCode, out var branch))
                    throw new Exception($"Branch '{record.BranchCode}' not found.");

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
