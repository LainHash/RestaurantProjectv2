using Restaurant.Domain.Enums;

namespace Restaurant.Seeding.DataRecords.Pricing
{
    internal class DiscountRecord
    {
        public string Name { get; set; } = string.Empty;

        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public decimal? MinimumOrderAmount { get; set; }

        public int TotalQuantity { get; set; }
        public int RemainingQuantity { get; set; }

        public bool IsActive { get; set; }


    }
}
