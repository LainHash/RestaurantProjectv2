using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetById
{
    internal class GetBrandByIdQueryHandler(IBrandService brandService)
                : IRequestHandler<GetBrandByIdQuery, Result<BrandDetailResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandDetailResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetBrandByIdSpecification(request);
            var response = await _brandService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
