using Restaurant.Contract.DTOs.Schedule.ReservationTables;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Schedule.Reservations
{
    public class CreateReservationRequest
    {
        public Guid BranchId { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }

        public int GuestCount { get; set; }

        public string? Note { get; set; }

        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        public string? GuestName { get; set; }
        public string? GuestPhone { get; set; }
        public string? GuestEmail { get; set; }

        public IEnumerable<CreateReservationTableRequest> ReservationTables { get; set; } = [];
    }
}
