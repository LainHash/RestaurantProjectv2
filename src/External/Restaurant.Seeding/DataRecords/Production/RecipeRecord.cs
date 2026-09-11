namespace Restaurant.Seeding.DataRecords.Production
{
    internal class RecipeRecord
    {
        public Guid PublicId { get; set; }
        public Guid ProductPublicId { get; set; }
        public string? Instructions { get; set; }
    }
}
