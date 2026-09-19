using AutoMapper;
using Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Create;
using Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Update;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Catalog
{
    internal class IngredientCategoryService : IIngredientCategoryService
    {
        private readonly IIngredientCategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IngredientCategoryService(
            IIngredientCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<IngredientCategoryResponse>>> GetAllAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _categoryRepository.CountAsync(specification, cancellationToken);

            var categories = await _categoryRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<IngredientCategoryResponse>>(categories);
            return PageResult<IEnumerable<IngredientCategoryResponse>>
                .Succeed(response, Success.Retrieved("IngredientCategory"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<IngredientCategoryResponse>> GetOneAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<IngredientCategoryResponse>
                    .Fail(Error.NotFound("IngredientCategory"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<IngredientCategoryResponse>(category);
            return Result<IngredientCategoryResponse>
                .Succeed(response, Success.Retrieved("IngredientCategory"));
        }

        public async Task<Result<IngredientCategoryResponse>> CreateAsync(
            CreateIngredientCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if (category is not null)
            {
                return Result<IngredientCategoryResponse>
                    .Fail("Ingredient Category with this name already exist.", HttpStatusCode.Conflict);
            }

            category = _mapper.Map<IngredientCategory>(command.Body);
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<IngredientCategoryResponse>(category);
            return Result<IngredientCategoryResponse>
                .Succeed(response, Success.Created("Ingredient Category"), HttpStatusCode.Created);
        }

        public async Task<Result<IngredientCategoryResponse>> UpdateAsync(
            UpdateIngredientCategoryCommand command,
            UpdateIngredientCategorySpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result<IngredientCategoryResponse>
                    .Fail(Error.NotFound("Ingredient Category"), HttpStatusCode.NotFound);
            }

            if (await _categoryRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<IngredientCategoryResponse>
                    .Fail("Ingredient Category with this name already exist.", HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<IngredientCategoryResponse>(category);
            return Result<IngredientCategoryResponse>
                .Succeed(response, Success.Updated("Ingredient Category"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error.NotFound("Ingredient Category"), HttpStatusCode.NotFound);
            }

            if (category.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Ingredient Category"), HttpStatusCode.BadRequest);
            }

            category.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Ingredient Category"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error.NotFound("Ingredient Category"), HttpStatusCode.NotFound);
            }

            if (!category.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Ingredient Category"), HttpStatusCode.BadRequest);
            }

            category.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Ingredient Category"));
        }
    }
}
