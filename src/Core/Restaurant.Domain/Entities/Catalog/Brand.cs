using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Brand : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public ICollection<Product> Products { get; private set; } = [];
        public ICollection<Ingredient> Ingredients { get; private set; } = [];
        public ICollection<BrandImage> BrandImages { get; private set;} = [];
    }
}
