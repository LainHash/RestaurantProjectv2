using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Domain.Repositories.Billing
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment?> FindByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Payment>> FindByInvoiceIdAsync(long invoiceId, CancellationToken cancellationToken = default);
    }
}
