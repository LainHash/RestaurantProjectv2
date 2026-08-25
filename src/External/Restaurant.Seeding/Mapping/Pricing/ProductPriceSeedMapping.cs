using AutoMapper;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Seeding.DataRecords.Pricing;

namespace Restaurant.Seeding.Mapping.Pricing
{
    internal class ProductPriceSeedMapping : Profile
    {
        public ProductPriceSeedMapping()
        {
            CreateMap<ProductPriceRecord, ProductPrice>();
        }
    }
}
