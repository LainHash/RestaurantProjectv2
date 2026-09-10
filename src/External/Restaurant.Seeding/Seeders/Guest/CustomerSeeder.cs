using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.DataRecords.Guest;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Seeders.Guest
{
    internal class CustomerSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Customers.AnyAsync())
                return;

            var users = await context.Users
                .Select(x => new { x.Id, x.PublicId })
                .ToListAsync();
            var usersDictionary = users.ToDictionary(
                x => x.PublicId);

            var records =
                _importer.Read<CustomerRecord>("Customers");

            foreach (var record in records)
            {
                if (!usersDictionary.TryGetValue(record.UserId, out var user))
                    throw new Exception($"User '{record.UserId}' not found.");

                var customer = _mapper.Map<Customer>(record)
                    .SetUser(user.Id);

                context.Customers.Add(customer);
            }

            await context.SaveChangesAsync();
        }
    }
}
