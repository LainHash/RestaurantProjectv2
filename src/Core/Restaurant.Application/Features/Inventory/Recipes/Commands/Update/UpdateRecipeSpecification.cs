using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.Recipes.Commands.Update
{
    public class UpdateRecipeSpecification
        : BaseSpecification<Recipe>
    {
        public UpdateRecipeSpecification(UpdateRecipeCommand command)
        {
            Criteria = r => r.PublicId == command.Id;

            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }
    }
}
