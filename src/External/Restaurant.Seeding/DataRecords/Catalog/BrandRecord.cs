namespace Restaurant.Seeding.DataRecords.Catalog
{
    internal class BrandRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
