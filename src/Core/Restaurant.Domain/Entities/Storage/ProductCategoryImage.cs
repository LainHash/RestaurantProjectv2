using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Storage
{
    public partial class ProductCategoryImage : AuditableEntity
    {
        public int ProductCategoryId { get; private set; }
        public int ImageId { get; private set; }

        public ProductCategory ProductCategory { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class ProductCategoryImage
    {
        public ProductCategoryImage() { }

        public ProductCategoryImage(int productCategoryId, int imageId)
        {
            ProductCategoryId = productCategoryId;
            ImageId = imageId;
        }

        public static ProductCategoryImage Create(int productCategoryId, int imageId)
        {
            return new ProductCategoryImage(productCategoryId, imageId);
        }

        public ProductCategoryImage SetProductCategory(int productCategoryId)
        {
            ProductCategoryId = productCategoryId;
            return this;
        }

        public ProductCategoryImage SetImage(int imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
