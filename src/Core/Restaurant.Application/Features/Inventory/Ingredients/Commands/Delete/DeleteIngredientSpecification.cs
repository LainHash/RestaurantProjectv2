using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.Ingredients.Commands.Delete
{
    public class DeleteIngredientSpecification
        : BaseSpecification<Ingredient>
    {
        public DeleteIngredientSpecification(DeleteIngredientCommand command)
        {
            Criteria = p => p.PublicId == command.Id;
        }
    }
}
