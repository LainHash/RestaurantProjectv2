using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByBranchId
{
    public class GetAllIngredientStockByBranchIdSpecification
        : BaseSpecification<IngredientStock>
    {
        public GetAllIngredientStockByBranchIdSpecification(GetAllIngredientStockByBranchIdQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Ingredient);
            AddInclude(x => x.Branch);

            Criteria = ps => ps.Branch.PublicId == query.BranchId;
        }
    }
}
