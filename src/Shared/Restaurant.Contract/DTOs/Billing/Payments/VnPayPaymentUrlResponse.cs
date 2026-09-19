namespace Restaurant.Contract.DTOs.Billing.Payments
{
    public class VnPayPaymentUrlResponse
    {
        public string PaymentUrl { get; set; } = string.Empty;
        public string TransactionCode { get; set; } = string.Empty;
        public Guid PaymentPublicId { get; set; }
        public decimal Amount { get; set; }
    }
}
