using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Storage
{
    public partial class ProductCategoryImage : AuditableEntity
    {
        public long ProductCategoryId { get; private set; }
        public long ImageId { get; private set; }

        public ProductCategory ProductCategory { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class ProductCategoryImage
    {
        public ProductCategoryImage() { }

        public ProductCategoryImage(long productCategoryId, long imageId)
        {
            ProductCategoryId = productCategoryId;
            ImageId = imageId;
        }

        public static ProductCategoryImage Create(long productCategoryId, long imageId)
        {
            return new ProductCategoryImage(productCategoryId, imageId);
        }

        public ProductCategoryImage SetProductCategory(long productCategoryId)
        {
            ProductCategoryId = productCategoryId;
            return this;
        }

        public ProductCategoryImage SetImage(long imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
