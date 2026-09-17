using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.Create
{
    public class CreateReservationSpecification
        : BaseSpecification<Reservation>
    {
        public CreateReservationSpecification()
        {
            AddInclude(x => x.Branch);

            AddIncludeAggregator(x => x.Include(r => r.ReservationTables)
                                        .ThenInclude(rt => rt.RestaurantTable));
        }

        public void ApplyCriteria(int id)
        {
            AddCriteria(x => x.Id == id);
        }
    }
}
