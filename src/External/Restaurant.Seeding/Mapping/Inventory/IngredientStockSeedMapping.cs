using AutoMapper;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Seeding.DataRecords.Inventory;

namespace Restaurant.Seeding.Mapping.Inventory
{
    internal class IngredientStockSeedMapping : Profile
    {
        public IngredientStockSeedMapping()
        {
            CreateMap<IngredientStockRecord, IngredientStock>();
        }
    }
}
