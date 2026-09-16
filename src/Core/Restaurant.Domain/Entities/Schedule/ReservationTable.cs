using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Schedule
{
    public class ReservationTable : SoftDeletableEntity
    {
        public int ReservationId { get; set; }
        public int RestaurantTableId { get; set; }

        public Reservation Reservation { get; set; } = null!;
        public RestaurantTable RestaurantTable { get; set; } = null!;
    }
}
