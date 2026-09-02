using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Territory
{
    public class Branch : SoftDeletableEntity
    {
        public string City { get; private set; } = null!;
        public string BranchCode { get; private set; } = null!;

        public string PhoneNumber { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        public string Address { get; private set; } = null!;

        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public BranchStatus Status { get; private set; }

        public TimeOnly OpenTime { get; private set; }
        public TimeOnly CloseTime { get; private set; }

        public ICollection<ProductStock> ProductStocks { get; private set; } = [];
        public ICollection<IngredientStock> IngredientStocks { get; private set; } = [];
        public ICollection<Order> Orders { get; private set; } = [];
    }
}
