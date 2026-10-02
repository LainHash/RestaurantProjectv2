using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Ingredients.Commands.Update
{
    internal class UpdateIngredientCommandHandler(IIngredientService ingredientService)
                : IRequestHandler<UpdateIngredientCommand, Result<IngredientResponse>>
    {
        private readonly IIngredientService _ingredientService = ingredientService;

        public async Task<Result<IngredientResponse>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateIngredientSpecification(request);
            var response = await _ingredientService.UpdateAsync(specification, request.Body, cancellationToken);
            return response;
        }
    }
}
