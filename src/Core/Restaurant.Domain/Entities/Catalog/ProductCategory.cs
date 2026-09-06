using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public class ProductCategory : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public ICollection<Product> Products { get; private set; } = [];
        public ICollection<ProductCategoryImage> ProductCategoryImages { get; private set; } = [];
    }
}
