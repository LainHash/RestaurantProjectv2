namespace Restaurant.Contract.DTOs.Inventory.IngredientCategories
{
    public class CreateIngredientCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
