using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Schedule
{
    public class Reservation : SoftDeletableEntity
    {
        public int BranchId { get; set; }

        public int? CustomerId { get; set; }

        public string? GuestName { get; set; }
        public string? GuestPhone { get; set; }
        public string? GuestEmail { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }

        public int GuestCount { get; set; }

        public ReservationStatus Status { get; set; }

        public string? Note { get; set; }

        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        public Branch Branch { get; set; } = null!;
        public Customer? Customer { get; set; }
        public ICollection<ReservationTable> ReservationTables { get; set; } = [];
    }
}
