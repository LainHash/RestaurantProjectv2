namespace Restaurant.Seeding.DataRecords.Catalog
{
    internal class IngredientRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UnitId { get; set; }
    }
}
