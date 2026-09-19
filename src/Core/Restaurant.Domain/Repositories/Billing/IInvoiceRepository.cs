using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Domain.Repositories.Billing
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<bool> HasInvoiceForOrderAsync(long orderId, CancellationToken cancellationToken = default);
        Task<Invoice?> FindByOrderIdAsync(long orderId, CancellationToken cancellationToken = default);
        Task<Invoice?> FindByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default);
    }
}
