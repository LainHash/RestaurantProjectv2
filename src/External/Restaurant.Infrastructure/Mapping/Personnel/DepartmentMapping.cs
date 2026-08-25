using AutoMapper;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Infrastructure.Mapping.Personnel
{
    internal class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateDepartmentRequest, Department>();

            CreateMap<UpdateDepartmentRequest, Department>();
        }
    }
}
