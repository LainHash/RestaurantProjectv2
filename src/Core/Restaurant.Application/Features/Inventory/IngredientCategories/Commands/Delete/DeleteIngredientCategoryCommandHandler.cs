using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Delete
{
    internal class DeleteIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<DeleteIngredientCategoryCommand, Result>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result> Handle(DeleteIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteIngredientCategorySpecification(request);
            var response = await _categoryService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
