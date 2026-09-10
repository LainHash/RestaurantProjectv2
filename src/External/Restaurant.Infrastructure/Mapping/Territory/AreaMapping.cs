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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.BranchCode : string.Empty));

            CreateMap<CreateAreaRequest, Area>();

            CreateMap<UpdateAreaRequest, Area>();
        }
    }
}
