using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class ProductDetailResponse
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
        public IEnumerable<ImageResponse> Images { get; set; } = [];
        public IEnumerable<RecipeResponse> Recipes { get; set; } = [];
    }
}
