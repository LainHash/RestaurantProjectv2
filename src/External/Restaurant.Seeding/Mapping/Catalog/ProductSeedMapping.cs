using AutoMapper;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;
using Restaurant.Seeding.DataRecords.Catalog;

namespace Restaurant.Seeding.Mapping.Catalog
{
    internal class ProductSeedMapping : Profile
    {
        public ProductSeedMapping()
        {
            CreateMap<ProductRecord, Product>()
                .ForMember(dest => dest.InventoryType, opt => opt.MapFrom(src => Enum.Parse<InventoryType>(src.InventoryType)));
        }
    }
}
