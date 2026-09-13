using AutoMapper;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Infrastructure.Mapping.Identity
{
    internal class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<RegisterRequest, User>();

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<User, UserDetailResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.PersonalProfile, opt => opt.MapFrom(src => src.PersonalProfile));
        }
    }
}
