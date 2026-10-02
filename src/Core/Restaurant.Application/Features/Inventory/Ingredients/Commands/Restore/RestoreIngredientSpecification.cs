using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.Ingredients.Commands.Restore
{
    public class RestoreIngredientSpecification
        : BaseSpecification<Ingredient>
    {
        public RestoreIngredientSpecification(RestoreIngredientCommand command)
        {
            Criteria = p => p.PublicId == command.Id;
        }
    }
}
