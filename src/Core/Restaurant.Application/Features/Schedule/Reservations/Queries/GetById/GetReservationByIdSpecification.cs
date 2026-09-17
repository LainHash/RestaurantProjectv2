using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetById
{
    public class GetReservationByIdSpecification
        : BaseSpecification<Reservation>
    {
        public GetReservationByIdSpecification(GetReservationByIdQuery query)
        {
            AddCriteria(x => x.PublicId == query.Id);

            AddInclude(x => x.Branch);
            AddIncludeAggregator(x => x.Include(r => r.ReservationTables)
                                        .ThenInclude(rt => rt.RestaurantTable));
        }
    }
}
