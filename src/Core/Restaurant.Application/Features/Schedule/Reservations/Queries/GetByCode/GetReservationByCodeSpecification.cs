using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetByCode
{
    public class GetReservationByCodeSpecification
        : BaseSpecification<Reservation>
    {
        public GetReservationByCodeSpecification(GetReservationByCodeQuery query)
        {
            AddCriteria(x => x.ReservationCode == query.Code);

            AddInclude(x => x.Branch);
            AddIncludeAggregator(x => x.Include(r => r.ReservationTables)
                                        .ThenInclude(rt => rt.RestaurantTable));
        }
    }
}
