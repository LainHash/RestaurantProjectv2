namespace Restaurant.Domain.Models.Messages
{
    public static class Error<TEntity> where TEntity : class
    {
        public readonly static string NotFound = $"{typeof(TEntity).Name} is not found.";

        public readonly static string NotYetDeleted = $"{typeof(TEntity).Name} has not yet been deleted.";

        public readonly static string AlreadyDeleted = $"{typeof(TEntity).Name}has already been deleted.";

        public readonly static string AlreadyAdded = $"{typeof(TEntity).Name} has already been added.";

        public readonly static string OutOfStock = $"{typeof(TEntity).Name} has run out of stock.";

        public readonly static string Occupied = $"{typeof(TEntity).Name} was occupied.";

        public readonly static string ExistedName = $"{typeof(TEntity).Name} with this name is already existed.";
    }
}
