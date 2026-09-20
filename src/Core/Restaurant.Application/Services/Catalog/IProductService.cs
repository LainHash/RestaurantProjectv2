using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAllPopular;
using Restaurant.Application.Features.Catalog.Products.Queries.GetById;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface IProductService
    {
        Task<PageResult<IEnumerable<ProductResponse>>> GetAllAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken = default);

        Task<PageResult<IEnumerable<PopularProductResponse>>> GetAllPopularAsync(
            GetAllPopularProductsSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ProductDetailResponse>> GetByIdAsync(
            GetProductByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ProductResponse>> CreateAsync(
            CreateProductSpecification specification,
            CreateProductRequest request,
            CancellationToken cancellationToken = default);

        Task<Result<ProductResponse>> UpdateAsync(
            UpdateProductSpecification specification,
            UpdateProductRequest request,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken = default);

        Task<Result> RestoreAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken = default);


    }
}
