using NanoidDotNet;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Schedule
{
    public partial class Reservation : SoftDeletableEntity
    {
        public long BranchId { get; private set; }

        public long? CustomerId { get; private set; }

        public string ReservationCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public string GuestName { get; private set; } = null!;
        public string GuestPhone { get; private set; } = null!;
        public string? GuestEmail { get; private set; }

        public DateOnly ReservationDate { get; private set; }
        public TimeOnly ReservationTime { get; private set; }
        public int Duration { get; private set; } = 2; //Hours

        public int GuestCount { get; private set; }

        public ReservationStatus Status { get; private set; } = ReservationStatus.Pending;

        public string? Note { get; private set; }

        public DateTime? ConfirmedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public string? CancellationReason { get; private set; }

        public Branch Branch { get; private set; } = null!;
        public Customer? Customer { get; private set; }
        public ICollection<ReservationTable> ReservationTables { get; private set; } = [];
    }

    public partial class Reservation
    {
        public Reservation() { }

        public Reservation(long branchId)
        {
            BranchId = branchId;
        }

        public Reservation SetBranch(long branchId)
        {
            BranchId = branchId;
            return this;
        }

        public Reservation SetGuest(Customer customer)
        {
            CustomerId = customer.Id;
            GuestName = customer.User.PersonalProfile!.FirstName + " " + customer.User.PersonalProfile!.LastName;
            GuestPhone = customer.User.PersonalProfile.Phone;
            GuestEmail = customer.User.Email;
            return this;
        }

        public Reservation SetReservationTables(IEnumerable<ReservationTable> reservationTables)
        {
            ReservationTables = [.. reservationTables];
            return this;
        }
    }
}
