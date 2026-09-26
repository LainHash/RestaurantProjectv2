namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class CreateRecipeRequest
    {
        public Guid ProductPublicId { get; set; }
        public string? Instructions { get; set; }
    }
}
