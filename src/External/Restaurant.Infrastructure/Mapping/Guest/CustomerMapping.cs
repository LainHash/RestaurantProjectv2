using AutoMapper;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Infrastructure.Mapping.Guest
{
    internal class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarImage != null ? src.AvatarImage.Url : ""))
                .ForMember(dest => dest.PersonalProfile, opt => opt.MapFrom(src => src.User.PersonalProfile));
        }
    }
}
