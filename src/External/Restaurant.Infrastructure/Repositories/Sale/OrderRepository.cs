using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Sale
{
    internal class OrderRepository(RestaurantDbContext context)
        : Repository<Order>(context), IOrderRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Order?> FindWithOrderDetailAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(x => x.OrderDetails)
                .Include(x => x.Invoice)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Order?> FindWithOrderDetailAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(x => x.OrderDetails)
                .Include(x => x.Invoice)
                .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
        }
    }
}
