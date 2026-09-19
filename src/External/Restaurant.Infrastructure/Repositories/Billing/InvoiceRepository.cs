using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Billing
{
    internal class InvoiceRepository(RestaurantDbContext context) 
        : Repository<Invoice>(context), IInvoiceRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> HasInvoiceForOrderAsync(long orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices
                .AnyAsync(x => x.OrderId == orderId, cancellationToken);
        }

        public async Task<Invoice?> FindByOrderIdAsync(long orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices
                .FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }
    }
}
