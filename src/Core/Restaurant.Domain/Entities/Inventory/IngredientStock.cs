using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Inventory
{
    public partial class IngredientStock : SoftDeletableEntity
    {
        public decimal QuantityOnHand { get; private set; }
        public decimal QuantityReserved { get; private set; }
        public decimal AvailableQuantity => QuantityOnHand - QuantityReserved;
        public decimal ReorderLevel { get; private set; }

        public long IngredientId { get; private set; }
        public long BranchId { get; private set; }

        public Ingredient Ingredient { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
    }

    public partial class IngredientStock
    {
        public IngredientStock() { }

        public IngredientStock(decimal quantityOnHand)
        {
            QuantityOnHand = quantityOnHand;
        }

        public IngredientStock SetIngredient(long ingredientId)
        {
            IngredientId = ingredientId;
            return this;
        }

        public IngredientStock SetBranch(long branchId)
        {
            BranchId = branchId;
            return this;
        }

        public void UpdateQuantity(decimal amount)
        {
            QuantityOnHand += amount;
        }

        public void Reserve(decimal amount)
        {
            QuantityReserved += amount;
        }

        public void Release(decimal amount)
        {
            QuantityReserved = Math.Max(0, QuantityReserved - amount);
        }
    }
}
