using Restaurant.Application.Features.Inventory.Recipes.Commands.AddIngredient;
using Restaurant.Application.Features.Inventory.Recipes.Commands.Create;
using Restaurant.Application.Features.Inventory.Recipes.Commands.Update;
using Restaurant.Application.Features.Inventory.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Inventory.Recipes.Queries.GetById;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IRecipeService
    {
        Task<Result<IEnumerable<RecipeResponse>>> GetAllAsync(
            GetAllRecipesSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> GetByIdAsync(
            GetRecipeByIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> CreateAsync(
            CreateRecipeCommand command,
            CreateRecipeSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> UpdateAsync(
            UpdateRecipeCommand command,
            UpdateRecipeSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> AddIngredientAsync(
            AddRecipeIngredientCommand command,
            AddRecipeIngredientSpecification specification,
            CancellationToken cancellationToken);
    }
}
