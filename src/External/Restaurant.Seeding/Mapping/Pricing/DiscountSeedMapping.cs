using AutoMapper;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Seeding.DataRecords.Pricing;

namespace Restaurant.Seeding.Mapping.Pricing
{
    internal class DiscountSeedMapping : Profile
    {
        public DiscountSeedMapping()
        {
            CreateMap<DiscountRecord, Discount>()
                .ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.EndAt, opt => opt.MapFrom(src => DateTime.UtcNow.AddYears(5)));
        }
    }
}
