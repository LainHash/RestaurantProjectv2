using Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Create;
using Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Update;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Inventory
{
    public interface IIngredientCategoryService
    {
        Task<PageResult<IEnumerable<IngredientCategoryResponse>>> GetAllAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientCategoryResponse>> GetOneAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientCategoryResponse>> CreateAsync(
            CreateIngredientCategoryCommand command,
            CancellationToken cancellationToken);

        Task<Result<IngredientCategoryResponse>> UpdateAsync(
            UpdateIngredientCategoryCommand command,
            UpdateIngredientCategorySpecification specification,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);
    }
}
