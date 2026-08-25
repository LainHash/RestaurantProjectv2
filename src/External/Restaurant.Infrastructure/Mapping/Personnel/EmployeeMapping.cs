using AutoMapper;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Infrastructure.Mapping.Personnel
{
    internal class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.User.Role.Name))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarImage!.Url))
                .ForMember(dest => dest.PositionCode, opt => opt.MapFrom(src => src.Position.PositionCode))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.PersonalProfile, opt => opt.MapFrom(src => src.User.PersonalProfile));
        }
    }
}
