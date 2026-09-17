using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Billing
{
    public class PaymentTransaction : SoftDeletableEntity
    {
        public long PaymentId { get; private set; }

        public string Provider { get; private set; } = null!;
        public string TransactionCode { get; private set; } = null!;

        public decimal Amount { get; private set; }

        public PaymentTransactionStatus Status { get; private set; }

        public string? RequestData { get; private set; }
        public string? ResponseData { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public Payment Payment { get; private set; } = null!;
    }
}
