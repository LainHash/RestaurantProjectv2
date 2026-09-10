namespace Restaurant.Seeding.DataRecords.Pricing
{
    internal class ProductPriceRecord
    {
        public Guid PublicId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = null!;

        public Guid ProductId { get; set; }
    }
}
