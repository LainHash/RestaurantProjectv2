using AutoMapper;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Mapping.Personnel
{
    internal class DepartmentSeedMapping : Profile
    {
        public DepartmentSeedMapping()
        {
            CreateMap<DepartmentRecord, Department>();
        }
    }
}
