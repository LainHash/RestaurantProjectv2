using AutoMapper;
using Restaurant.Application.Features.Catalog.ProductCategories.Commands.Create;
using Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update;
using Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Catalog
{
    internal class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(
            IProductCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<ProductCategoryResponse>>> GetAllAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _categoryRepository.CountAsync(specification, cancellationToken);

            var categories = await _categoryRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ProductCategoryResponse>>(categories);
            return PageResult<IEnumerable<ProductCategoryResponse>>
                .Succeed(response, Success.Retrieved("Product Category"), totalItems, specification.Skip, specification.Take);

        }

        public async Task<Result<ProductCategoryDetailResponse>> GetByIdAsync(
            GetProductCategoryByIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<ProductCategoryDetailResponse>
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ProductCategoryDetailResponse>(category);
            return Result<ProductCategoryDetailResponse>
                .Succeed(response, Success.Retrieved("Product Category"));
        }

        public async Task<Result<ProductCategoryResponse>> CreateAsync(
            CreateProductCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if(category is not null)
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error.ExistedName("Product Category"), HttpStatusCode.Conflict);
            }

            category = _mapper.Map<ProductCategory>(command.Body);
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductCategoryResponse>(category);
            return Result<ProductCategoryResponse>
                .Succeed(response, Success.Created("Product Category"), HttpStatusCode.Created);
        }

        public async Task<Result<ProductCategoryResponse>> UpdateAsync(
            UpdateProductCategoryCommand command,
            UpdateProductCategorySpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            if(await _categoryRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error.ExistedName("Product Category"), HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductCategoryResponse>(category);
            return Result<ProductCategoryResponse>
                .Succeed(response, Success.Updated("Product Category"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            if(category.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Product Category"), HttpStatusCode.BadRequest);
            }

            category.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Product Category"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            if(!category.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Product Category"), HttpStatusCode.BadRequest);
            }

            category.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Product Category"));
        }
    }
}
