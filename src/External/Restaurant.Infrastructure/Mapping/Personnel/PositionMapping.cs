using AutoMapper;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Infrastructure.Mapping.Personnel
{
    internal class PositionMapping : Profile
    {
        public PositionMapping()
        {
            CreateMap<Position, PositionResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreatePositionRequest, Position>()
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore());

            CreateMap<UpdatePositionRequest, Position>()
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore());
        }
    }
}
