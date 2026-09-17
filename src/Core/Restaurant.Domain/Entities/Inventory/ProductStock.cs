using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Inventory
{
    public partial class ProductStock : SoftDeletableEntity
    {
        public decimal QuantityOnHand { get; private set; }

        public long ProductId { get; private set; }
        public long BranchId { get; private set; }

        public Product Product { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
    }

    public partial class ProductStock
    {
        public ProductStock() { }

        public ProductStock(long productId, long branchId)
        {
            ProductId = productId;
            BranchId = branchId;
        }

        public ProductStock(decimal quantityOnHand)
        {
            QuantityOnHand = quantityOnHand;
        }

        public ProductStock SetProduct(long productId)
        {
            ProductId = productId;
            return this;
        }

        public ProductStock SetBranch(long branchId)
        {
            BranchId = branchId;
            return this;
        }

        public ProductStock(decimal quantityOnHand, long productId, long branchId)
        {
            QuantityOnHand = quantityOnHand;
            ProductId = productId;
            BranchId = branchId;
        }
        public void UpdateQuantity(decimal amount)
        {
            QuantityOnHand += amount;
        }
    }
}
