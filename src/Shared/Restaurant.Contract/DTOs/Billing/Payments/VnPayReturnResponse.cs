namespace Restaurant.Contract.DTOs.Billing.Payments
{
    public class VnPayReturnResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string TransactionCode { get; set; } = string.Empty;
        public string? VnPayTransactionNo { get; set; }
        public decimal Amount { get; set; }
        public string? BankCode { get; set; }
        public string? ResponseCode { get; set; }
        public DateTime? PayDate { get; set; }
    }
}
