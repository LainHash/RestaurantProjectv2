using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Territory
{
    public partial class Area : SoftDeletableEntity
    {
        public int BranchId { get; private set; }

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }

        public Branch Branch { get; private set; } = null!;
        public ICollection<RestaurantTable> RestaurantTables { get; private set; } = [];
    }

    public partial class Area
    {
        public Area() { }

        public Area SetBranch(int branchId)
        {
            BranchId = branchId;
            return this;
        }
    }
}
