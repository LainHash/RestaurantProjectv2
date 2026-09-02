namespace Restaurant.Contract.DTOs.Catalog.Ingredients
{
    public class CreateIngredientRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid? BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UnitId { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
