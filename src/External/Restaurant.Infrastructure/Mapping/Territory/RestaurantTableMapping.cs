using AutoMapper;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Infrastructure.Mapping.Territory
{
    internal class RestaurantTableMapping : Profile
    {
        public RestaurantTableMapping()
        {
            CreateMap<RestaurantTable, RestaurantTableResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<RestaurantTable, RestaurantTableMinimalResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateRestaurantTableRequest, RestaurantTable>();

            CreateMap<UpdateRestaurantTableRequest, RestaurantTable>();
        }
    }
}
