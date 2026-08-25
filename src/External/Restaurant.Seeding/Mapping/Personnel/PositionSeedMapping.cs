using AutoMapper;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Seeding.DataRecords.Personnel;

namespace Restaurant.Seeding.Mapping.Personnel
{
    internal class PositionSeedMapping : Profile
    {
        public PositionSeedMapping()
        {
            CreateMap<PositionRecord, Position>();
        }
    }
}
