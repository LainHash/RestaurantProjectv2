namespace Restaurant.Seeding.DataRecords.Inventory
{
    internal class UnitRecord
    {
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public decimal ConversionRate { get; set; }
    }
}
