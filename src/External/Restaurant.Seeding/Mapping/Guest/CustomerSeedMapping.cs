using AutoMapper;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Seeding.DataRecords.Guest;

namespace Restaurant.Seeding.Mapping.Guest
{
    internal class CustomerSeedMapping : Profile
    {
        public CustomerSeedMapping()
        {
            CreateMap<CustomerRecord, Customer>();
        }
    }
}
