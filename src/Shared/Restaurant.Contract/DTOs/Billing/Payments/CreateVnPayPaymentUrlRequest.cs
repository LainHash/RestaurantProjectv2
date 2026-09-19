namespace Restaurant.Contract.DTOs.Billing.Payments
{
    public class CreateVnPayPaymentUrlRequest
    {
        public Guid InvoiceId { get; set; }
        public string? BankCode { get; set; }
    }
}
