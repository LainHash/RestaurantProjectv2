using AutoMapper;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Infrastructure.Mapping.Territory
{
    internal class AreaMapping : Profile
    {
        public AreaMapping()
        {
            CreateMap<Area, AreaResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<Area, AreaDetailResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.RestaurantTables, opt => opt.MapFrom(src => src.RestaurantTables));

            CreateMap<CreateAreaRequest, Area>();

            CreateMap<UpdateAreaRequest, Area>();
        }
    }
}
