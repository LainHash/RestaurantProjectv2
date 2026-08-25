using AutoMapper;
using Restaurant.Domain.Entities.Production;
using Restaurant.Seeding.DataRecords.Production;

namespace Restaurant.Seeding.Mapping.Production
{
    internal class RecipeSeedMapping : Profile
    {
        public RecipeSeedMapping()
        {
            CreateMap<RecipeRecord, Recipe>();
        }
    }
}
