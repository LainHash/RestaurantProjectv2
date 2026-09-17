using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Territory
{
    public partial class RestaurantTable : SoftDeletableEntity
    {
        public long AreaId { get; private set; }

        public string TableNumber { get; private set; } = null!;
        public int Capacity { get; private set; }
        public TableShape Shape { get; private set; }
        public TableStatus Status { get; private set; }

        public decimal PositionX { get; private set; }
        public decimal PositionY { get; private set; }

        public decimal Width { get; private set; }
        public decimal Height { get; private set; }

        public int Rotation { get; private set; }

        public bool IsActive { get; private set; }

        public Area Area { get; private set; } = null!;
        public ICollection<ReservationTable> ReservationTables { get; private set; } = [];
    }

    public partial class RestaurantTable
    {
        public RestaurantTable() { }

        public RestaurantTable SetArea(long areaId)
        {
            AreaId = areaId;
            return this;
        }
    }
}
