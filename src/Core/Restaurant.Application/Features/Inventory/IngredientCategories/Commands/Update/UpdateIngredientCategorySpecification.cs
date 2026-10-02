using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Update
{
    public class UpdateIngredientCategorySpecification
        : BaseSpecification<IngredientCategory>
    {
        public UpdateIngredientCategorySpecification(UpdateIngredientCategoryCommand command)
        {
            Criteria = c => c.PublicId == command.Id;
        }
    }
}
