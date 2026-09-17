using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Storage
{
    public partial class BrandImage : AuditableEntity
    {
        public long BrandId { get; private set; }
        public long ImageId { get; private set; }

        public Brand Brand { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class BrandImage
    {
        public BrandImage() { }

        public BrandImage(long brandId, long imageId)
        {
            BrandId = brandId;
            ImageId = imageId;
        }

        public static BrandImage Create(long brandId, long imageId)
        {
            return new BrandImage(brandId, imageId);
        }

        public BrandImage SetBrand(long brandId)
        {
            BrandId = brandId;
            return this;
        }

        public BrandImage SetImage(long imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
