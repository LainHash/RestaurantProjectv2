using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Billing
{
    internal class PaymentRepository(RestaurantDbContext context)
        : Repository<Payment>(context), IPaymentRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Payment?> FindByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(x => x.Invoice)
                .Include(x => x.PaymentTransactions)
                .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
        }

        public async Task<IReadOnlyList<Payment>> FindByInvoiceIdAsync(long invoiceId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(x => x.PaymentTransactions)
                .Where(x => x.InvoiceId == invoiceId)
                .ToListAsync(cancellationToken);
        }
    }
}
