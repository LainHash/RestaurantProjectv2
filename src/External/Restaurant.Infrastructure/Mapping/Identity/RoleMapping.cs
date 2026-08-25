using AutoMapper;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Infrastructure.Mapping.Identity
{
    internal class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateRoleRequest, Role>();
            CreateMap<UpdateRoleRequest, Role>();
        }
    }
}
