using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll
{
    public class GetAllReservationsSpecification
        : BaseSpecification<Reservation>
    {
        public GetAllReservationsSpecification(GetAllReservationsQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(rt =>
                    EF.Functions.Like(rt.GuestCount.ToString(), $"%{query.Keyword}%") ||
                    EF.Functions.Like(rt.Note, $"%{query.Keyword}%") ||
                    EF.Functions.Like(nameof(rt.Status), $"%{query.Keyword}%"));
            }

            if (query.FromDate.HasValue)
            {
                AddCriteria(r => r.ReservationDate >= query.FromDate.Value);
            }

            if (query.ToDate.HasValue)
            {
                AddCriteria(r => r.ReservationDate <= query.ToDate.Value);
            }

            switch (query.SortField)
            {
                case SortField.Capacity:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.GuestCount);
                    else
                        ApplyOrderByDescending(p => p.GuestCount);
                    break;
                case SortField.ReservationDate:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.ReservationDate);
                    else
                        ApplyOrderByDescending(p => p.ReservationDate);
                    break;
                default:
                    ApplyOrderByDescending(p => p.ReservationDate);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
