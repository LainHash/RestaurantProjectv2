using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Pricing
{
    public partial class DiscountCustomer : SoftDeletableEntity
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

    public partial class DiscountCustomer
    {
        public DiscountCustomer() { }

        public DiscountCustomer(
            int customerId,
            int discountId)
        {
            CustomerId = customerId;
            DiscountId = discountId;
        }

        public DiscountCustomer(
            int customerId,
            int discountId,
            DateTime claimAt,
            DateTime expiredAt,
            DiscountCustomerStatus status)
            : this(customerId, discountId)
        {
            ClaimAt = claimAt;
            ExpiredAt = expiredAt;
            Status = status;
        }

        public static DiscountCustomer Claim(int customerId, int discountId)
        {
            return new DiscountCustomer(
                customerId,
                discountId,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(30),
                DiscountCustomerStatus.Available);
        }
    }
}
