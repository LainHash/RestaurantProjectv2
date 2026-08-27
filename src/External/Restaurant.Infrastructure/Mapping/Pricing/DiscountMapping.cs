using AutoMapper;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Infrastructure.Mapping.Pricing
{
    internal class DiscountMapping : Profile
    {
        public DiscountMapping()
        {
            CreateMap<Discount, DiscountResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));
        }
    }
}
