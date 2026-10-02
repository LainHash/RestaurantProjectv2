using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.Recipes.Queries.GetById
{
    public class GetRecipeByIdSpecification
        : BaseSpecification<Recipe>
    {
        public GetRecipeByIdSpecification(GetRecipeByIdQuery query)
        {
            EnableSoftDeleteFilter();

            Criteria = r => r.PublicId == query.Id;

            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }
    }
}
