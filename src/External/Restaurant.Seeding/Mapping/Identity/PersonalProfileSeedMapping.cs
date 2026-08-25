using AutoMapper;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Seeding.DataRecords.Identity;

namespace Restaurant.Seeding.Mapping.Identity
{
    internal class PersonalProfileSeedMapping : Profile
    {
        public PersonalProfileSeedMapping()
        {
            CreateMap<PersonalProfileRecord, PersonalProfile>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
        }
    }
}
