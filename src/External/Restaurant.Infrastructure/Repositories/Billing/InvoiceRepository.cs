using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Billing
{
    internal class InvoiceRepository(RestaurantDbContext context) 
        : Repository<Invoice>(context), IInvoiceRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
