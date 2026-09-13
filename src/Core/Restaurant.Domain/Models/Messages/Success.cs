namespace Restaurant.Domain.Models.Messages
{
    public static class Success<TEntity> where TEntity : class
    {
        public readonly static string Retrieved = $"{typeof(TEntity).Name} was retrived successfully.";

        public readonly static string Created = $"{typeof(TEntity).Name} was created successfully.";
        public readonly static string Updated = $"{typeof(TEntity).Name} was updated successfully.";

        public readonly static string Deleted = $"{typeof(TEntity).Name} was deleted successfully.";
        public readonly static string Restored = $"{typeof(TEntity).Name} was restored successfully.";

        public readonly static string Uploaded = $"{typeof(TEntity).Name} was uploaded successfully.";
        public readonly static string Added = $"{typeof(TEntity).Name} was added successfully.";
    }
}
