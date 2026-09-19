using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Commerce
{
    public partial class CartItem : AuditableEntity
    {
        public long CartId { get; private set; }
        public Cart Cart { get; private set; } = null!;

        public long ProductId { get; private set; }
        public Product Product { get; private set; } = null!;

        public int Quantity { get; private set; }
    }

    public partial class CartItem
    {
        public CartItem() { }
        public CartItem(long cartId, long productId)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = 1;
        }

        public void UpdateQuantity(int quantity = 1)
        {
            Quantity = quantity;
        }
    }
}
