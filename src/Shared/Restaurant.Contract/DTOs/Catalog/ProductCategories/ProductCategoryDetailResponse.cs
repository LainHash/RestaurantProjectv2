using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Contract.DTOs.Storage.Images;

namespace Restaurant.Contract.DTOs.Catalog.ProductCategories
{
    public class ProductCategoryDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public IEnumerable<ImageResponse> Images { get; set; } = [];
        public IEnumerable<ProductMinimalResponse> Products { get; set; } = [];
    }
}
