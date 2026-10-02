namespace Restaurant.Contract.DTOs.Inventory.Recipes
{
    public class UpdateRecipeRequest
    {
        public Guid ProductPublicId { get; set; }
        public string? Instructions { get; set; }
    }
}
