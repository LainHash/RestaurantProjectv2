using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Billing
{
    public class Payment : SoftDeletableEntity
    {
        public long InvoiceId { get; private set; }

        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "VND";

        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }

        public DateTime? PaidAt { get; private set; }

        public Invoice Invoice { get; private set; } = null!;
        public ICollection<PaymentTransaction> PaymentTransactions { get; private set; } = [];
    }
}
