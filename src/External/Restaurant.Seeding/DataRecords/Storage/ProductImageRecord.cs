namespace Restaurant.Seeding.DataRecords.Storage
{
    internal class ProductImageRecord
    {
        public Guid PublicId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }

        public Guid ProductId { get; set; }
        public Guid ImageId { get; set; }
    }
}
