using AutoMapper;
using Restaurant.Application.Features.General.Homes.Queries.Get;
using Restaurant.Application.Services.General;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Contract.DTOs.General;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Territory;

namespace Restaurant.Infrastructure.Services.General
{
    internal class HomeService : IHomeService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IBranchRepository _branchRepository;

        private readonly IMapper _mapper;

        public HomeService(
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IBranchRepository branchRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<Result<HomeResponse>> GetAsync(
            GetHomeQuery query,
            CancellationToken cancellationToken = default)
        {
            var popularProducts = await _productRepository.FindPopularProductsAsync(query.ProductLimit, cancellationToken);
            var productCategories = await _productCategoryRepository.ToListAsync(cancellationToken);
            var branches = await _branchRepository.ToListAsync(cancellationToken);

            var response = new HomeResponse()
            {
                PopularProducts = _mapper.Map<IEnumerable<PopularProductResponse>>(popularProducts),
                ProductCategories = _mapper.Map<IEnumerable<ProductCategoryResponse>>(productCategories),
                Branches = _mapper.Map<IEnumerable<BranchResponse>>(branches)
            };
            return Result<HomeResponse>
                .Succeed(response, Success.Retrieved("Home Data"));
        }
    }
}
