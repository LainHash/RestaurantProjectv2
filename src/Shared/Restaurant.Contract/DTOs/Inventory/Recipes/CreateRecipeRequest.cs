namespace Restaurant.Contract.DTOs.Inventory.Recipes
{
    public class CreateRecipeRequest
    {
        public Guid ProductPublicId { get; set; }
        public string? Instructions { get; set; }
    }
}
