using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Commerce
{
    public partial class WishlistItem : AuditableEntity
    {
        public long WishlistId { get; private set; }
        public Wishlist Wishlist { get; private set; } = null!;

        public long ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
    }

    public partial class WishlistItem
    {
        public WishlistItem() { }

        public WishlistItem(long wishlistId, long productId)
        {
            WishlistId = wishlistId;
            ProductId = productId;
        }
    }
}
