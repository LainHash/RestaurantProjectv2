using AutoMapper;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Seeding.DataRecords.Storage;

namespace Restaurant.Seeding.Mapping.Storage
{
    internal class ImageSeedMapping : Profile
    {
        public ImageSeedMapping()
        {
            CreateMap<ImageRecord, Image>();
        }
    }
}
