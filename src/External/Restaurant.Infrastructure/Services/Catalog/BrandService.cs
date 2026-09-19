using AutoMapper;
using Restaurant.Application.Features.Catalog.Brands.Commands.Create;
using Restaurant.Application.Features.Catalog.Brands.Commands.Update;
using Restaurant.Application.Features.Catalog.Brands.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Catalog
{
    internal class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(
            IBrandRepository brandRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<BrandResponse>>> GetAllAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _brandRepository.CountAsync(specification, cancellationToken);

            var brands = await _brandRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<BrandResponse>>(brands);
            return PageResult<IEnumerable<BrandResponse>>
                .Succeed(response, Success.Retrieved("Brand"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<BrandDetailResponse>> GetByIdAsync(
            GetBrandByIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand == null)
            {
                return Result<BrandDetailResponse>
                    .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<BrandDetailResponse>(brand);
            return Result<BrandDetailResponse>
                .Succeed(response, Success.Retrieved("Brand"));
        }

        public async Task<Result<BrandResponse>> CreateAsync(
            CreateBrandCommand command,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if (brand is not null)
            {
                return Result<BrandResponse>
                    .Fail(Error.ExistedName("Brand"), HttpStatusCode.Conflict);
            }

            brand = _mapper.Map<Brand>(command.Body);
            _brandRepository.Add(brand);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>
                .Succeed(response, Success.Created("Brand"), HttpStatusCode.Created);
        }

        public async Task<Result<BrandResponse>> UpdateAsync(
            UpdateBrandCommand command,
            UpdateBrandSpecification specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand is null)
            {
                return Result<BrandResponse>
                    .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
            }

            if (await _brandRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<BrandResponse>
                    .Fail(Error.ExistedName("Brand"), HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, brand);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>
                .Succeed(response, Success.Updated("Brand"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand == null)
            {
                return Result
                    .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
            }

            if (brand.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Brand"), HttpStatusCode.BadRequest);
            }

            brand.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Brand"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand == null)
            {
                return Result
                    .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
            }

            if (!brand.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Brand"), HttpStatusCode.BadRequest);
            }

            brand.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Brand"));
        }
    }
}
