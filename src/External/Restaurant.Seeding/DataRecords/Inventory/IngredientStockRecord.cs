namespace Restaurant.Seeding.DataRecords.Inventory
{
    internal class IngredientStockRecord
    {
        public Guid PublicId { get; set; }
        public decimal QuantityOnHand { get; set; }

        public Guid IngredientPublicId { get; set; }
        public Guid BranchPublicId { get; set; }
    }
}
