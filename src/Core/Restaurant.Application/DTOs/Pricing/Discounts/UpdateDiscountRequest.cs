using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Pricing.Discounts
{
    public class UpdateDiscountRequest
    {
        public string Name { get; set; } = string.Empty;

        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public decimal? MinimumOrderAmount { get; set; }

        public int TotalQuantity { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
