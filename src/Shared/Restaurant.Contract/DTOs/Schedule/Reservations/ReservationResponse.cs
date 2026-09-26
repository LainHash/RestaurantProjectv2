using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Schedule.Reservations
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }

        public string ReservationCode { get; set; } = null!;

        public string BranchCode { get; set; } = null!;

        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int Duration { get; set; }

        public int GuestCount { get; set; }

        public ReservationStatus Status { get; set; }

        public string? Note { get; set; }
    }
}
