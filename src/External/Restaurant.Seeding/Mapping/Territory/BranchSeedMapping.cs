using AutoMapper;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Seeding.DataRecords.Territory;

namespace Restaurant.Seeding.Mapping.Territory
{
    internal class BranchSeedMapping : Profile
    {
        public BranchSeedMapping()
        {
            CreateMap<BranchRecord, Branch>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BranchStatus>(src.Status)))
                .ForMember(dest => dest.OpenTime, opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.OpenTime)))
                .ForMember(dest => dest.CloseTime, opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.CloseTime)));
        }
    }
}
