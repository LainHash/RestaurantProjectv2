using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }

        public string? BrandName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = string.Empty;

        public ImageResponse PrimaryImage { get; set; } = null!;
    }
}
