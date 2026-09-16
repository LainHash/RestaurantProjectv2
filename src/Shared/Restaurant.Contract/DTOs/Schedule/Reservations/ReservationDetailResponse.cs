using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Contract.DTOs.Schedule.ReservationTables;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Schedule.Reservations
{
    public class ReservationDetailResponse
    {
        public Guid Id { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }

        public int GuestCount { get; set; }

        public ReservationStatus Status { get; set; }

        public string? Note { get; set; }

        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        public string GuestName { get; set; } = null!;
        public string GuestPhone { get; set; } = null!;
        public string GuestEmail { get; set; } = null!;

        public IEnumerable<ReservationTableResponse> ReservationTables { get; set; } = [];
    }
}
