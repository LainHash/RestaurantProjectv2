using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Pricing
{
    public partial class ProductPrice : SoftDeletableEntity
    {
        public decimal UnitPrice { get; private set; }
        public string Currency { get; private set; } = null!;

        public long ProductId { get; private set; }

        public Product Product { get; private set; } = null!;
    }

    public partial class ProductPrice
    {
        public ProductPrice() { }

        public ProductPrice(decimal unitPrice, string currency)
        {
            UnitPrice = unitPrice;
            Currency = currency;
        }

        public ProductPrice(decimal unitPrice, long productId)
        {
            UnitPrice = unitPrice;
            ProductId = productId;
        }

        public ProductPrice SetProduct(long productId)
        {
            ProductId = productId;
            return this;
        }
    }
}
