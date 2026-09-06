using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Storage
{
    public partial class BrandImage : AuditableEntity
    {
        public int BrandId { get; private set; }
        public int ImageId { get; private set; }

        public Brand Brand { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class BrandImage
    {
        public BrandImage() { }

        public BrandImage(int brandId, int imageId)
        {
            BrandId = brandId;
            ImageId = imageId;
        }

        public static BrandImage Create(int brandId, int imageId)
        {
            return new BrandImage(brandId, imageId);
        }

        public BrandImage SetBrand(int brandId)
        {
            BrandId = brandId;
            return this;
        }

        public BrandImage SetImage(int imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
