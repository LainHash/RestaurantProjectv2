using Restaurant.Contract.DTOs.Billing.InvoiceDetails;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Billing.Invoices
{
    public class InvoiceResponse
    {
        public Guid Id { get; set; }
        public string InvoiceCode { get; set; } = null!;
        public string OrderCode { get; set; } = null!;

        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public InvoiceStatus Status { get; set; }

        public DateTime? IssuedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public IEnumerable<InvoiceDetailResponse> InvoiceDetails { get; set; } = [];
    }
}
