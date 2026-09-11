namespace Restaurant.Seeding.DataRecords.Catalog
{
    internal class IngredientRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public Guid BrandPublicId { get; set; }
        public Guid IngredientCategoryPublicId { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
