using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Domain.Repositories.Billing
{
    public interface IPaymentTransactionRepository : IRepository<PaymentTransaction>
    {
        Task<PaymentTransaction?> FindByTransactionCodeWithPaymentAsync(string transactionCode, CancellationToken cancellationToken = default);
    }
}
