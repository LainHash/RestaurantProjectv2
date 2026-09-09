using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Territory.RestaurantTables
{
    public class RestaurantTableResponse
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public TableShape Shape { get; set; }
        public TableStatus Status { get; set; }
    }
}
