using NanoidDotNet;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Billing
{
    public partial class Invoice : SoftDeletableEntity
    {
        public long OrderId { get; private set; }

        public string InvoiceCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public decimal Subtotal { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public InvoiceStatus Status { get; private set; }

        public DateTime? IssuedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }

        public Order Order { get; private set; } = null!;
        public ICollection<InvoiceDetail> InvoiceDetails { get; private set; } = [];
        public ICollection<Payment> Payments { get; private set; } = [];
    }

    public partial class Invoice
    {
        public Invoice() { }

        public Invoice(
            decimal subtotal,
            decimal discountAmount,
            decimal taxAmount,
            decimal totalAmount)
        {
            Subtotal = subtotal;
            DiscountAmount = discountAmount;
            TaxAmount = taxAmount;
            TotalAmount = totalAmount;
            Status = InvoiceStatus.Draft;
        }

        public Invoice(Order order)
            : this(order.Subtotal, order.DiscountAmount, order.TaxAmount, order.TotalAmount)
        {
            OrderId = order.Id;
            InvoiceDetails = [.. order.OrderDetails.Select(x => new InvoiceDetail(x))];
        }

        public void Issue()
        {
            IssuedAt = DateTime.UtcNow;
        }

        public void MarkAsPaid(DateTime? paidAt = null)
        {
            Status = InvoiceStatus.Paid;
            PaidAt = paidAt ?? DateTime.UtcNow;
        }

        public void MarkAsPartiallyPaid()
        {
            Status = InvoiceStatus.PartiallyPaid;
        }

        public void Cancel()
        {
            Status = InvoiceStatus.Cancelled;
        }

        public void UpdateAmounts(decimal subtotal, decimal discountAmount, decimal taxAmount, decimal totalAmount)
        {
            Subtotal = subtotal;
            DiscountAmount = discountAmount;
            TaxAmount = taxAmount;
            TotalAmount = totalAmount;
        }

        public void AddInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            InvoiceDetails.Add(invoiceDetail);
        }
    }
}
