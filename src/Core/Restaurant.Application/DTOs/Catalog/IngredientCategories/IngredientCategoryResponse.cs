namespace Restaurant.Contract.DTOs.Catalog.IngredientCategories
{
    public class IngredientCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
