using AutoMapper;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Enums;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Mapping.Personnel
{
    internal class EmployeeSeedMapping : Profile
    {
        public EmployeeSeedMapping()
        {
            CreateMap<EmployeeRecord, Employee>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<EmployeeStatus>(src.Status)))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate.ToUniversalTime()));
        }
    }
}
