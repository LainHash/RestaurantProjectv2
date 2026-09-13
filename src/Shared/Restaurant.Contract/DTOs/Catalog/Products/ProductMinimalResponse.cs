using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class ProductMinimalResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }
    }
}
