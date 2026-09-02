using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public class OrderDiscount : SoftDeletableEntity
    {
        public int OrderId { get; private set; }
        public int DiscountId { get; private set; }

        public string DiscountCode { get; private set; } = null!;
        public string DiscountType { get; private set; } = null!;
        public decimal DiscountValue { get; private set; }
        public decimal DiscountAmount { get; private set; }

        public Order Order { get; private set; } = null!;
        public Discount Discount { get; private set; } = null!;
    }
}
