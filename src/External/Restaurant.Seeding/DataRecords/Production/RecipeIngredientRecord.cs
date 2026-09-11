namespace Restaurant.Seeding.DataRecords.Production
{
    internal class RecipeIngredientRecord
    {
        public Guid PublicId { get; set; }
        public Guid RecipePublicId { get; set; }
        public Guid IngredientPublicId { get; set; }
        public decimal Quantity { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
