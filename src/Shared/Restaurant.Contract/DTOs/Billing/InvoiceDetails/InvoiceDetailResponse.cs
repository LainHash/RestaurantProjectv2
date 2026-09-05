namespace Restaurant.Contract.DTOs.Billing.InvoiceDetails
{
    public class InvoiceDetailResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
