using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Queries.GetById
{
    public class GetIngredientCategoryByIdSpecification
        : BaseSpecification<IngredientCategory>
    {
        public GetIngredientCategoryByIdSpecification(GetIngredientCategoryByIdQuery query)
        {
            Criteria = category => category.PublicId == query.Id;

            EnableSoftDeleteFilter();
        }
    }
}
