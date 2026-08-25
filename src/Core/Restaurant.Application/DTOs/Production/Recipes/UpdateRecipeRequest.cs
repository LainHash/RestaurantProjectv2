namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class UpdateRecipeRequest
    {
        public Guid ProductId { get; set; }
        public string? Instructions { get; set; }
    }
}
