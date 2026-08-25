using AutoMapper;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Seeding.DataRecords.Catalog;

namespace Restaurant.Seeding.Mapping.Catalog
{
    internal class IngredientSeedMapping : Profile
    {
        public IngredientSeedMapping()
        {
            CreateMap<IngredientRecord, Ingredient>();
        }
    }
}
