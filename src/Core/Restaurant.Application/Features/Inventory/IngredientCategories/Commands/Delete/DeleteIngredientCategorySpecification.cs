using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Delete
{
    public class DeleteIngredientCategorySpecification
        : BaseSpecification<IngredientCategory>
    {
        public DeleteIngredientCategorySpecification(DeleteIngredientCategoryCommand command)
        {
            Criteria = category => category.PublicId == command.Id;
        }
    }
}
