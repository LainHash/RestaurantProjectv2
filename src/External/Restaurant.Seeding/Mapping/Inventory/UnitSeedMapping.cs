using AutoMapper;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Enums;
using Restaurant.Seeding.DataRecords.Inventory;

namespace Restaurant.Seeding.Mapping.Inventory
{
    internal class UnitSeedMapping : Profile
    {
        public UnitSeedMapping()
        {
            CreateMap<UnitRecord, Unit>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<UnitType>(src.Type)));
        }
    }
}
