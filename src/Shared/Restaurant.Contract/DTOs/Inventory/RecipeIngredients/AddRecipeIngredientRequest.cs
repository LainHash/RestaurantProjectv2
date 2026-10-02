namespace Restaurant.Contract.DTOs.Inventory.RecipeIngredients
{
    public class AddRecipeIngredientRequest
    {
        public Guid IngredientPublicId { get; set; }
        public decimal Quantity { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
