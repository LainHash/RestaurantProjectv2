using NanoidDotNet;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Pricing
{
    public partial class Discount : SoftDeletableEntity
    {
        public string Name { get;  private set; } = string.Empty;
        public string DiscountCode { get; private set; } = Nanoid.Generate("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);

        public DiscountType Type { get;  private set; }
        public decimal Value { get;  private set; }
        public decimal? MaximumDiscountAmount { get;  private set; }
        public decimal? MinimumOrderAmount { get;  private set; }

        public int TotalQuantity { get; private set; }
        public int RemainingQuantity { get; private set; }

        public DateTime StartAt { get;  private set; }
        public DateTime EndAt { get;  private set; }
        public bool IsActive { get;  private set; }

        public ICollection<DiscountCustomer> DiscountCustomers { get; private set; } = [];
        public ICollection<OrderDiscount> OrderDiscounts { get; private set; } = [];
    }

    public partial class Discount
    {
        public Discount() { }

        public Discount UpdateQuantity(int amount = 1)
        {
            RemainingQuantity += amount;
            return this;
        }
    }
}
