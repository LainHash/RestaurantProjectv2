using AutoMapper;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Seeding.DataRecords.Identity;

namespace Restaurant.Seeding.Mapping.Identity
{
    internal class RoleSeedMapping : Profile
    {
        public RoleSeedMapping()
        {
            CreateMap<RoleRecord, Role>();
        }
    }
}
