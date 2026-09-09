using AutoMapper;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Infrastructure.Mapping.Territory
{
    internal class BranchMapping : Profile
    {
        public BranchMapping()
        {
            CreateMap<Branch, BranchResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));
        }
    }
}
