using AutoMapper;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Seeding.DataRecords.Pricing;

namespace Restaurant.Seeding.Mapping.Pricing
{
    internal class IngredientPriceSeedMapping : Profile
    {
        public IngredientPriceSeedMapping()
        {
            CreateMap<IngredientPriceRecord, IngredientPrice>();
        }
    }
}
