using AutoMapper;
using Restaurant.Contract.DTOs.Schedule.ReservationTables;
using Restaurant.Domain.Entities.Schedule;

namespace Restaurant.Infrastructure.Mapping.Schedule
{
    public class ReservationTableMapping : Profile
    {
        public ReservationTableMapping()
        {
            CreateMap<ReservationTable, ReservationTableResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.RestaurantTableId, opt => opt.MapFrom(src => src.RestaurantTable.PublicId))
                .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.RestaurantTable.TableNumber))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.RestaurantTable.Capacity))
                .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.RestaurantTable.Shape))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RestaurantTable.Status));
        }
    }
}
