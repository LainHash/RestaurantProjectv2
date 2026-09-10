namespace Restaurant.Contract.DTOs.Catalog.Ingredients
{
    public class IngredientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? BrandName { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
