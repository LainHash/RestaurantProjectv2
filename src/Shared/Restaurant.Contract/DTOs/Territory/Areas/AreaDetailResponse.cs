using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Contract.DTOs.Territory.Areas
{
    public class AreaDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<RestaurantTable> RestaurantTables { get; set; } = [];
    }
}
