using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Restore
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
