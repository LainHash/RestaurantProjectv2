using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.Recipes.Queries.GetAll
{
    public class GetAllRecipesSpecification
        : BaseSpecification<Recipe>
    {
        public GetAllRecipesSpecification(GetAllRecipesQuery query)
        {
            EnableSoftDeleteFilter();
            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }
    }
}
