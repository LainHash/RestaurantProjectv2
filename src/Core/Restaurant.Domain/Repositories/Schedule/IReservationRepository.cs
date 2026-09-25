using Restaurant.Domain.Entities.Schedule;

namespace Restaurant.Domain.Repositories.Schedule
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
