namespace Restaurant.Contract.DTOs.Inventory.RecipeIngredients
{
    public class RecipeIngredientResponse
    {
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
