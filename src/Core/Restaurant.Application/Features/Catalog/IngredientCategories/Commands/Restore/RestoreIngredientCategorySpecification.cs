using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Restore
{
    public class RestoreIngredientCategorySpecification
        : BaseSpecification<IngredientCategory>
    {
        public RestoreIngredientCategorySpecification(RestoreIngredientCategoryCommand command)
        {
            Criteria = category => category.PublicId == command.Id;
        }
    }
}
