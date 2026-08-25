using AutoMapper;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Seeding.DataRecords.Inventory;

namespace Restaurant.Seeding.Mapping.Inventory
{
    internal class ProductStockSeedMapping : Profile
    {
        public ProductStockSeedMapping()
        {
            CreateMap<ProductStockRecord, ProductStock>();
        }
    }
}
