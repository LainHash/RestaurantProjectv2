using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetById
{
    internal class GetProductCategoryByIdQueryHandler(IProductCategoryService categoryService)
                : IRequestHandler<GetProductCategoryByIdQuery, Result<ProductCategoryDetailResponse>>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result<ProductCategoryDetailResponse>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetProductCategoryByIdSpecification(request);
            var response = await _categoryService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
