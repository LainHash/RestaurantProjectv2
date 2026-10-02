using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Queries.GetById
{
    internal class GetIngredientCategoryByIdQueryHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<GetIngredientCategoryByIdQuery, Result<IngredientCategoryResponse>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result<IngredientCategoryResponse>> Handle(GetIngredientCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetIngredientCategoryByIdSpecification(request);
            var response = await _categoryService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
