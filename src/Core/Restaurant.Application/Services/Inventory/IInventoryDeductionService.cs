using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IInventoryDeductionService
    {
        Result ReserveInventoryForOrder(
            long branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default);

        Result ReleaseReservedInventoryForOrder(
            long branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default);
    }
}
