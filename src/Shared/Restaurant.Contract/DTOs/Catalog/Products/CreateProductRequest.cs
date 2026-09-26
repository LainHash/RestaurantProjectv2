using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }

        public Guid? BrandPublicId { get; set; }
        public Guid CategoryPublicId { get; set; }
        public Guid UnitPublicId { get; set; }

        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
