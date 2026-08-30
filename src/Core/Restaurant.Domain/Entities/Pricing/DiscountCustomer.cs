using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Pricing
{
    public class DiscountCustomer : SoftDeletableEntity
    {
        public int CustomerId { get; private set; }
        public int DiscountId { get; private set; }

        public DateTime ClaimAt { get; private set; }
        public DateTime ExpiredAt { get; private set; }
        public DateTime? UsedAt { get; private set; }
        public DiscountCustomerStatus Status { get; private set; }

        public Customer Customer { get; private set; } = null!;
        public Discount Discount { get; private set; } = null!;
    }
}
