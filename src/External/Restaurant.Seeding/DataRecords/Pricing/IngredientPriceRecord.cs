namespace Restaurant.Seeding.DataRecords.Pricing
{
    internal class IngredientPriceRecord
    {
        public Guid PublicId { get; set; }
        public decimal UnitPrice { get; set; }

        public Guid IngredientId{ get; set; }
    }
}
