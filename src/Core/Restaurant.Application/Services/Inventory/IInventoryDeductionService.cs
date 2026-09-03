using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IInventoryDeductionService
    {
        Task<Result> DeductInventoryForOrderAsync(
            int branchId,
            IEnumerable<(Product Product, int Quantity)> items,
            CancellationToken cancellationToken = default);
    }
}
