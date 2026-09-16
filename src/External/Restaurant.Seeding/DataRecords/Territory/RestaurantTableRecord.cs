using Restaurant.Domain.Enums;

namespace Restaurant.Seeding.DataRecords.Territory
{
    internal class RestaurantTableRecord
    {
        public Guid PublicId { get; set; }
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public TableShape Shape { get; set; }
        public TableStatus Status { get; set; }

        public decimal PositionX { get; set; }
        public decimal PositionY { get; set; }

        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public int Rotation { get; set; }

        public bool IsActive { get; set; }

        public Guid AreaPublicId { get; set; }
    }
}
