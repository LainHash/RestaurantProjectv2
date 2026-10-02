using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Recipes.Commands.AddIngredient
{
    internal class AddRecipeIngredientCommandHandler(IRecipeService recipeService)
                : IRequestHandler<AddRecipeIngredientCommand, Result<RecipeResponse>>
    {
        private readonly IRecipeService _recipeService = recipeService;

        public async Task<Result<RecipeResponse>> Handle(AddRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            var specification = new AddRecipeIngredientSpecification(request);
            var response = await _recipeService.AddIngredientAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
