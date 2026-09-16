using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Schedule.ReservationTables
{
    public class ReservationTableResponse
    {
        public Guid Id { get; set; }

        public Guid RestaurantTableId { get; set; }
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public TableShape Shape { get; set; }
        public TableStatus Status { get; set; }
    }
}
