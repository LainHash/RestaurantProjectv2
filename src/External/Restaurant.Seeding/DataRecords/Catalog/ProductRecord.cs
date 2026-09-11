namespace Restaurant.Seeding.DataRecords.Catalog
{
    internal class ProductRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string InventoryType { get; set; } = null!;

        public Guid BrandPublicId { get; set; }
        public Guid ProductCategoryPublicId { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
