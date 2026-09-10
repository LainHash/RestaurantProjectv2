namespace Restaurant.Seeding.DataRecords.Production
{
    internal class RecipeRecord
    {
        public Guid PublicId { get; set; }
        public Guid ProductId { get; set; }
        public string? Instructions { get; set; }
    }
}
