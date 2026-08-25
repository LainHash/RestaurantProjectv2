using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity
{
    public class UpdateIngredientStockQuantitySpecification
        : BaseSpecification<IngredientStock>
    {
        public UpdateIngredientStockQuantitySpecification(UpdateIngredientStockQuantityCommand command)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Ingredient);
            AddInclude(x => x.Branch);

            Criteria = ps => ps.Ingredient.PublicId == command.IngredientId
                            && ps.Branch.PublicId == command.BranchId;
        }
    }
}
