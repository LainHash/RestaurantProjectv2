using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Territory
{
    public class RestaurantTable : SoftDeletableEntity
    {
        public int AreaId { get; private set; }

        public string TableNumber { get; private set; } = null!;
        public int Capacity { get; private set; }
        public TableShape Shape { get; private set; }
        public TableStatus Status { get; private set; }

        public bool IsActive { get; private set; }

        public Area Area { get; private set; } = null!;
    }
}
