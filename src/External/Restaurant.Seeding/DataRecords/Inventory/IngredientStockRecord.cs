namespace Restaurant.Seeding.DataRecords.Inventory
{
    internal class IngredientStockRecord
    {
        public Guid PublicId { get; set; }
        public decimal QuantityOnHand { get; set; }

        public Guid IngredientId { get; set; }
        public Guid BranchId { get; set; }
    }
}
