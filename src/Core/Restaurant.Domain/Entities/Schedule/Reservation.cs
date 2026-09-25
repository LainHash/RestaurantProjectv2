using NanoidDotNet;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Schedule
{
    public partial class Reservation : SoftDeletableEntity
    {
        public long BranchId { get; set; }

        public long? CustomerId { get; set; }

        public string ReservationCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public string GuestName { get; set; } = null!;
        public string GuestPhone { get; set; } = null!;
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
            GuestName = customer.User.PersonalProfile!.FirstName + " " + customer.User.PersonalProfile!.LastName;
            GuestPhone = customer.User.PersonalProfile.Phone;
            GuestEmail = customer.User.Email;
            return this;
        }
    }
}
