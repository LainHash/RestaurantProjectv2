namespace Restaurant.Seeding.DataRecords.Inventory
{
    internal class ProductStockRecord
    {
        public Guid PublicId { get; set; }
        public decimal QuantityOnHand { get; set; }

        public Guid ProductPublicId { get; set; }
        public Guid BranchPublicId { get; set; }
    }
}
