using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Queries.GetAll
{
    internal class GetAllIngredientCategoriesQueryHandler(IIngredientCategoryService categoryService)
        : IRequestHandler<GetAllIngredientCategoriesQuery, PageResult<IEnumerable<IngredientCategoryResponse>>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<PageResult<IEnumerable<IngredientCategoryResponse>>> Handle(GetAllIngredientCategoriesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllIngredientCategoriesSpecification(request);
            var response = await _categoryService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
