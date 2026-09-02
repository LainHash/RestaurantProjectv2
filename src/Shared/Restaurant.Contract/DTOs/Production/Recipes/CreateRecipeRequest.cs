namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class CreateRecipeRequest
    {
        public Guid ProductId { get; set; }
        public string? Instructions { get; set; }
    }
}
