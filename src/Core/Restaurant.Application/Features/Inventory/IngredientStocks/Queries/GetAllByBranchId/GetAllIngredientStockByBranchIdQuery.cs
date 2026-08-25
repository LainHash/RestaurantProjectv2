using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByBranchId
{
    public record GetAllIngredientStockByBranchIdQuery(Guid BranchId)
        : IRequest<Result<IEnumerable<IngredientStockResponse>>>
    {
    }
}
