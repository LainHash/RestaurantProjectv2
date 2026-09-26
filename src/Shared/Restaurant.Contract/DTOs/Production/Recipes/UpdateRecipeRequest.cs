namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class UpdateRecipeRequest
    {
        public Guid ProductPublicId { get; set; }
        public string? Instructions { get; set; }
    }
}
