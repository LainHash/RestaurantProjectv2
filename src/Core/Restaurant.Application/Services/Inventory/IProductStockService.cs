using Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IProductStockService
    {
        Task<Result<ProductStockResponse>> UpdateQuantityAsync(
            UpdateProductStockQuantityCommand command,
            UpdateProductStockQuantitySpecification specification,
            CancellationToken cancellationToken);
    }
}
