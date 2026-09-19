using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Billing
{
    internal class PaymentTransactionRepository(RestaurantDbContext context)
        : Repository<PaymentTransaction>(context), IPaymentTransactionRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<PaymentTransaction?> FindByTransactionCodeWithPaymentAsync(string transactionCode, CancellationToken cancellationToken = default)
        {
            return await _context.PaymentTransactions
                .Include(x => x.Payment)
                    .ThenInclude(p => p.Invoice)
                        .ThenInclude(i => i.Order)
                            .ThenInclude(o => o.OrderDetails)
                                .ThenInclude(od => od.OrderPreparation)
                .FirstOrDefaultAsync(x => x.TransactionCode == transactionCode, cancellationToken);
        }
    }
}
