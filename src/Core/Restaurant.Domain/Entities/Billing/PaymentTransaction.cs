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

        public PaymentTransaction() { }

        public PaymentTransaction(
            long paymentId,
            string provider,
            string transactionCode,
            decimal amount,
            string? requestData = null)
        {
            PaymentId = paymentId;
            Provider = provider;
            TransactionCode = transactionCode;
            Amount = amount;
            RequestData = requestData;
            Status = PaymentTransactionStatus.Pending;
        }

        public void Success(string responseData, DateTime? completedAt = null)
        {
            Status = PaymentTransactionStatus.Success;
            ResponseData = responseData;
            CompletedAt = completedAt ?? DateTime.UtcNow;
        }

        public void Failed(string responseData, DateTime? completedAt = null)
        {
            Status = PaymentTransactionStatus.Failed;
            ResponseData = responseData;
            CompletedAt = completedAt ?? DateTime.UtcNow;
        }

        public void Cancelled(string responseData, DateTime? completedAt = null)
        {
            Status = PaymentTransactionStatus.Cancelled;
            ResponseData = responseData;
            CompletedAt = completedAt ?? DateTime.UtcNow;
        }
    }
}
