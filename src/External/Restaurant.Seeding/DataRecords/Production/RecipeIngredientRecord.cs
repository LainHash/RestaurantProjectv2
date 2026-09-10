namespace Restaurant.Seeding.DataRecords.Production
{
    internal class RecipeIngredientRecord
    {
        public Guid PublicId { get; set; }
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public Guid UnitId { get; set; }
    }
}
