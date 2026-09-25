using Restaurant.Contract.DTOs.Schedule.ReservationTables;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Schedule.Reservations
{
    public class ReservationDetailResponse
    {
        public Guid Id { get; set; }

        public string BranchCode { get; set; } = null!;
        public string ReservationCode { get; set; } = null!;

        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }

        public int GuestCount { get; set; }

        public ReservationStatus Status { get; set; }

        public string? Note { get; set; }

        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        public string GuestName { get; set; } = null!;
        public string GuestPhone { get; set; } = null!;
        public string? GuestEmail { get; set; }

        public IEnumerable<ReservationTableResponse> ReservationTables { get; set; } = [];
    }
}
