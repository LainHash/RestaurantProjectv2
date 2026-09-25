using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Repositories.Schedule;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Schedule
{
    internal class ReservationRepository(RestaurantDbContext context) 
        : Repository<Reservation>(context), IReservationRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Reservation?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .FirstOrDefaultAsync(x => x.ReservationCode == code, cancellationToken);
        }
    }
}
