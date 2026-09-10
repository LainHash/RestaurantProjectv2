using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Pricing
{
    public partial class IngredientPrice : SoftDeletableEntity
    {
        public decimal UnitPrice { get; private set; }
        public string Currency { get; private set; } = null!;

        public int IngredientId { get; private set; }



        public Ingredient Ingredient { get; private set; } = null!;
    }

    public partial class IngredientPrice
    {
        public IngredientPrice SetIngredient(int ingredientId)
        {
            IngredientId = ingredientId;
            return this;
        }
    }
}
