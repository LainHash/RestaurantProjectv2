using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Schedule
{
    public class ReservationTable : SoftDeletableEntity
    {
        public long ReservationId { get; set; }
        public long RestaurantTableId { get; set; }

        public Reservation Reservation { get; set; } = null!;
        public RestaurantTable RestaurantTable { get; set; } = null!;
    }
}
