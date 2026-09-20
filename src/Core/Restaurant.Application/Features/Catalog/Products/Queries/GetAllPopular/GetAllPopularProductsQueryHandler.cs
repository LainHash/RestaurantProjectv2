using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAllPopular
{
    internal class GetAllPopularProductsQueryHandler(IProductService productService)
                : IRequestHandler<GetAllPopularProductsQuery, PageResult<IEnumerable<PopularProductResponse>>>
    {
        private readonly IProductService _productService = productService;

        public async Task<PageResult<IEnumerable<PopularProductResponse>>> Handle(GetAllPopularProductsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllPopularProductsSpecification(request);
            var response = await _productService.GetAllPopularAsync(specification, cancellationToken);
            return response;
        }
    }
}
