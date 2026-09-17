using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Pricing
{
    public partial class DiscountCustomer : SoftDeletableEntity
    {
        public long CustomerId { get; private set; }
        public long DiscountId { get; private set; }

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
            long customerId,
            long discountId)
        {
            CustomerId = customerId;
            DiscountId = discountId;
        }

        public DiscountCustomer(
            long customerId,
            long discountId,
            DateTime claimAt,
            DateTime expiredAt,
            DiscountCustomerStatus status)
            : this(customerId, discountId)
        {
            ClaimAt = claimAt;
            ExpiredAt = expiredAt;
            Status = status;
        }

        public static DiscountCustomer Claim(long customerId, long discountId, DateTime expiredAt)
        {
            return new DiscountCustomer(
                customerId,
                discountId,
                DateTime.UtcNow,
                expiredAt,
                DiscountCustomerStatus.Available);
        }
    }
}
