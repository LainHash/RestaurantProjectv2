using Restaurant.Contract.DTOs.Inventory.RecipeIngredients;

namespace Restaurant.Contract.DTOs.Inventory.Recipes
{
    public class RecipeResponse
    {
        public string Id { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public string? Instructions { get; set; }

        public IEnumerable<RecipeIngredientResponse> RecipeIngredients { get; set; } = [];
    }
}
