using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.UpdateByCustomer
{
    public class UpdateReservationByCustomerSpecification
        : BaseSpecification<Reservation>
    {
        public UpdateReservationByCustomerSpecification(UpdateReservationByCustomerCommand command)
        {
            AddCriteria(x => x.PublicId == command.Id);
        }
    }
}
