namespace Restaurant.Contract.DTOs.Catalog.Ingredients
{
    public class CreateIngredientRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid? BrandPublicId { get; set; }
        public Guid CategoryPublicId { get; set; }
        public Guid UnitPublicId { get; set; }

        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
