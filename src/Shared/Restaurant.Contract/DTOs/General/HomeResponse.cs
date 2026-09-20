using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Contract.DTOs.Territory.Branches;

namespace Restaurant.Contract.DTOs.General
{
    public class HomeResponse
    {
        public IEnumerable<PopularProductResponse> PopularProducts { get; set; } = [];
        public IEnumerable<ProductCategoryResponse> ProductCategories { get; set; } = [];
        public IEnumerable<BranchResponse> Branches { get; set; } = [];
    }
}
