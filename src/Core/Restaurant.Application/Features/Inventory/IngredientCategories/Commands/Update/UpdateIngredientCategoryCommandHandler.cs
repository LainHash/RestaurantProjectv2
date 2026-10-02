using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Update
{
    internal class UpdateIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<UpdateIngredientCategoryCommand, Result<IngredientCategoryResponse>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result<IngredientCategoryResponse>> Handle(UpdateIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateIngredientCategorySpecification(request);
            var response = await _categoryService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
