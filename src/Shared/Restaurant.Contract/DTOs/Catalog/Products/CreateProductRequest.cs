using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }

        public Guid? BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UnitId { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
