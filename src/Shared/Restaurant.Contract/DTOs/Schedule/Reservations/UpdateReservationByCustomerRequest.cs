namespace Restaurant.Contract.DTOs.Schedule.Reservations
{
    public class UpdateReservationByCustomerRequest
    {
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int Duration { get; set; }

        public int GuestCount { get; set; }

        public string? Note { get; set; }

        public string? GuestName { get; set; }
        public string? GuestPhone { get; set; }
        public string? GuestEmail { get; set; }
    }
}
