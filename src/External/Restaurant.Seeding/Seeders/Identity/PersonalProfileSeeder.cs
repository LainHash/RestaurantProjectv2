using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Guest;
using Restaurant.Seeding.DataRecords.Identity;

namespace Restaurant.Seeding.Seeders.Identity
{
    internal class PersonalProfileSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.PersonalProfiles.AnyAsync())
                return;

            var users = await context.Users
                .Select(x => new { x.Id, x.UserName })
                .ToListAsync();
            var usersDictionary = users.ToDictionary(
                x => x.UserName,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<PersonalProfileRecord>("PersonalProfiles");

            foreach (var record in records)
            {
                if (!usersDictionary.TryGetValue(record.UserName.ToLower(), out var user))
                    throw new Exception($"User '{record.UserName}' not found.");

                var personalProfile = _mapper.Map<PersonalProfile>(record)
                    .SetUser(user.Id);

                context.PersonalProfiles.Add(personalProfile);
            }

            await context.SaveChangesAsync();
        }
    }
}
