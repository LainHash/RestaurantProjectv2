using AutoMapper;
using Restaurant.Application.DTOs.Identity.PersonalProfiles;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Infrastructure.Mapping.Identity
{
    internal class PersonalProfileMapping : Profile
    {
        public PersonalProfileMapping()
        {
            CreateMap<CompleteProfileRequest, PersonalProfile>();

            CreateMap<PersonalProfile, PersonalProfileResponse>();

            CreateMap<CreatePersonalProfileRequest, PersonalProfile>();

            CreateMap<UpdatePersonalProfileRequest, PersonalProfile>();
        }
    }
}
