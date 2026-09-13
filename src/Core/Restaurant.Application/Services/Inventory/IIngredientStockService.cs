using Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IIngredientStockService
    {
        Task<Result<IngredientStockResponse>> UpdateQuantityAsync(
            UpdateIngredientStockQuantityCommand command,
            UpdateIngredientStockQuantitySpecification specification,
            CancellationToken cancellationToken);
    }
}
