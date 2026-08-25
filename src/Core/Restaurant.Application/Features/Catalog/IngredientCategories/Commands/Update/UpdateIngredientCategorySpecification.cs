using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Update
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
