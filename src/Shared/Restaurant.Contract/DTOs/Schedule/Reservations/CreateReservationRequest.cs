using Restaurant.Contract.DTOs.Schedule.ReservationTables;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Schedule.Reservations
{
    public class CreateReservationRequest
    {
        public Guid BranchId { get; set; }

        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }

        public int GuestCount { get; set; }

        public string? Note { get; set; }

        public string? GuestName { get; set; }
        public string? GuestPhone { get; set; }
        public string? GuestEmail { get; set; }

        public IEnumerable<CreateReservationTableRequest> ReservationTables { get; set; } = [];
    }
}
