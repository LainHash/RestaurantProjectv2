using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.Recipes.Commands.Create
{
    public class CreateRecipeSpecification
        : BaseSpecification<Recipe>
    {
        public CreateRecipeSpecification(CreateRecipeCommand command)
        {
            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }

        public void ApplyCriteria(long id)
        {
            Criteria = r => r.Id == id;
        }
    }
}
