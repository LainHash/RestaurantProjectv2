using NanoidDotNet;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Billing
{
    public class Invoice : SoftDeletableEntity
    {
        public int OrderId { get; private set; }

        public string InvoiceCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public decimal Subtotal { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public InvoiceStatus Status { get; private set; }

        public DateTime IssuedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }

        public Order Order { get; private set; } = null!;
        public ICollection<InvoiceDetail> InvoiceDetails { get; private set; } = [];
        public ICollection<Payment> Payments { get; private set; } = [];
    }
}
