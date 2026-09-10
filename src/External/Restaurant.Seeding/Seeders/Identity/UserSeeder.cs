using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Identity;

namespace Restaurant.Seeding.Seeders.Identity
{
    internal class UserSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var roles = await context.Roles
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var rolesDictionary = roles.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<UserRecord>("Users");

            foreach (var record in records)
            {
                if (!rolesDictionary.TryGetValue(record.RoleId, out var role))
                    throw new Exception($"Role '{record.RoleId}' not found.");

                var user = _mapper.Map<User>(record)
                    .SetRole(role.Id);

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
