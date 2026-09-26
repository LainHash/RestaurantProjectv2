namespace Restaurant.Contract.DTOs.Production.RecipeIngredients
{
    public class AddRecipeIngredientRequest
    {
        public Guid IngredientPublicId { get; set; }
        public decimal Quantity { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
