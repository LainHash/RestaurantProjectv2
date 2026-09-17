using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Production
{
    public partial class RecipeIngredient : SoftDeletableEntity
    {
        public long RecipeId { get; private set; }
        public long IngredientId { get; private set; }

        public decimal Quantity { get; private set; }

        public long UnitId { get; private set; }

        public Recipe Recipe { get; private set; } = null!;
        public Ingredient Ingredient { get; private set; } = null!;
        public Unit Unit { get; private set; } = null!;
    }

    public partial class RecipeIngredient
    {
        public RecipeIngredient SetIngredient(long ingredientId)
        {
            IngredientId = ingredientId;
            return this;
        }

        public RecipeIngredient SetRecipe(long recipeId)
        {
            RecipeId = recipeId;
            return this;
        }

        public RecipeIngredient SetUnit(long unitId)
        {
            UnitId = unitId;
            return this;
        }
    }
}
