using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Schedule
{
    public partial class ReservationTable : SoftDeletableEntity
    {
        public long ReservationId { get; private set; }
        public long RestaurantTableId { get; private set; }

        public Reservation Reservation { get; private set; } = null!;
        public RestaurantTable RestaurantTable { get; private set; } = null!;
    }

    public partial class ReservationTable
    {
        public ReservationTable() { }

        public ReservationTable(long restaurantTableId)
        {
            RestaurantTableId = restaurantTableId;
        }
    }
}
