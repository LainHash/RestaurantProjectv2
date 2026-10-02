using Restaurant.Application.Features.Inventory.Ingredients.Commands.Create;
using Restaurant.Application.Features.Inventory.Ingredients.Commands.Update;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Inventory
{
    public interface IIngredientService
    {
        Task<PageResult<IEnumerable<IngredientResponse>>> GetAllAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientResponse>> GetByIdAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientResponse>> CreateAsync(
            CreateIngredientSpecification specification,
            CreateIngredientRequest request,
            CancellationToken cancellationToken);

        Task<Result<IngredientResponse>> UpdateAsync(
            UpdateIngredientSpecification specification,
            UpdateIngredientRequest request,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);
    }
}
