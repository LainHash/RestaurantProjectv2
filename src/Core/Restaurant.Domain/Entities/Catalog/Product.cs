using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public partial class Product : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public InventoryType InventoryType { get; private set; }

        public long? BrandId { get; private set; }
        public long CategoryId { get; private set; }
        public long UnitId { get; private set; }

        public Brand? Brand { get; private set; }
        public ProductCategory ProductCategory { get; private set; } = null!;
        public Unit Unit { get; private set; } = null!;

        public ProductPrice ProductPrice { get; private set; } = null!;
        public ICollection<ProductStock> ProductStocks { get; private set; } = [];
        public ICollection<ProductImage> ProductImages { get; private set; } = [];

        public ICollection<Recipe> Recipes { get; private set; } = [];

        public ICollection<WishlistItem> WishlistItems { get; private set; } = [];
        public ICollection<CartItem> CartItems { get; private set; } = [];
        public ICollection<OrderDetail> OrderDetails { get; private set; } = [];
        public ICollection<InvoiceDetail> InvoiceDetails { get; private set; } = [];
    }

    public partial class Product
    {
        public Product SetBrand(long? brandId)
        {
            BrandId = brandId;
            return this;
        }

        public Product SetCategory(long categoryId)
        {
            CategoryId = categoryId;
            return this;
        }

        public Product SetUnit(long unitId)
        {
            UnitId = unitId;
            return this;
        }
    }
}
