using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Restore
{
    internal class RestoreIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<RestoreIngredientCategoryCommand, Result>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result> Handle(RestoreIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreIngredientCategorySpecification(request);
            var response = await _categoryService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
