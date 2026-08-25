using AutoMapper;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Seeding.DataRecords.Identity;

namespace Restaurant.Seeding.Mapping.Identity
{
    internal class UserSeedMapping : Profile
    {
        public UserSeedMapping()
        {
            CreateMap<UserRecord, User>();
        }
    }
}
