using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Ingredients.Queries.GetAll
{
    internal class GetAllIngredientsQueryHandler(IIngredientService ingredientService)
                : IRequestHandler<GetAllIngredientsQuery, PageResult<IEnumerable<IngredientResponse>>>
    {
        private readonly IIngredientService _ingredientService = ingredientService;

        public async Task<PageResult<IEnumerable<IngredientResponse>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllIngredientsSpecification(request);
            var response = await _ingredientService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
