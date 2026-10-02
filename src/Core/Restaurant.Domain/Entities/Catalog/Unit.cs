using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Unit : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string Symbol { get; private set; } = null!;

        public UnitType Type { get; private set; }

        public decimal ConversionRate { get; private set; }

        public ICollection<Product> Products { get; private set; } = [];
        public ICollection<Ingredient> Ingredients { get; private set; } = [];
        public ICollection<RecipeIngredient> RecipeIngredients { get; private set; } = [];
    }
}
