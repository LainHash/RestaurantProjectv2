namespace Restaurant.Seeding.DataRecords.Catalog
{
    internal class IngredientCategoryRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
