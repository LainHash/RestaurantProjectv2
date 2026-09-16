using AutoMapper;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Entities.Schedule;

namespace Restaurant.Infrastructure.Mapping.Schedule
{
    internal class ReservationMapping : Profile
    {
        public ReservationMapping()
        {
            CreateMap<Reservation, ReservationResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<Reservation, ReservationDetailResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.ReservationTables, opt => opt.MapFrom(src => src.ReservationTables));
        }
    }
}
