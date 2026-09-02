namespace Restaurant.Contract.DTOs.Production.RecipeIngredients
{
    public class AddRecipeIngredientRequest
    {
        public Guid IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public Guid UnitId { get; set; }
    }
}
