using AutoMapper;
using Restaurant.Domain.Entities.Production;
using Restaurant.Seeding.DataRecords.Production;

namespace Restaurant.Seeding.Mapping.Production
{
    internal class RecipeIngredientSeedMapping : Profile
    {
        public RecipeIngredientSeedMapping()
        {
            CreateMap<RecipeIngredientRecord, RecipeIngredient>();
        }
    }
}
