using AutoMapper;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Create;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Update;
using Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Pricing;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Pricing
{
    internal class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(
            IDiscountRepository discountRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageResult<IEnumerable<DiscountResponse>>> GetAllAsync(
            GetAllDiscountsSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _discountRepository.CountAsync(specification, cancellationToken);

            var discounts = await _discountRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<DiscountResponse>>(discounts);
            return PageResult<IEnumerable<DiscountResponse>>
                .Succeed(
                response,
                Success<Discount>.Retrieved,
                totalItems,
                specification.Skip,
                specification.Take);
        }

        public async Task<Result<DiscountResponse>> CreateAsync(
            CreateDiscountCommand command,
            CancellationToken cancellationToken = default)
        {
            var discount = _mapper.Map<Discount>(command.Body);
            _discountRepository.Add(discount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DiscountResponse>(discount);
            return Result<DiscountResponse>
                .Succeed(response, Success<Discount>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<DiscountResponse>> UpdateAsync(
            UpdateDiscountCommand command,
            UpdateDiscountSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var discount = await _discountRepository.FindAsync(specification, cancellationToken);
            if (discount is null)
            {
                return Result<DiscountResponse>
                    .Fail(Error<Discount>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(command.Body, discount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DiscountResponse>(discount);
            return Result<DiscountResponse>
                .Succeed(response, Success<Discount>.Updated, HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Discount> specification,
            CancellationToken cancellationToken = default)
        {
            var discount = await _discountRepository.FindAsync(specification, cancellationToken);
            if (discount == null)
            {
                return Result
                    .Fail(Error<Discount>.NotFound, HttpStatusCode.NotFound);
            }

            if (discount.IsDeleted)
            {
                return Result
                    .Fail(Error<Discount>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            discount.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<Discount>.Deleted);
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Discount> specification,
            CancellationToken cancellationToken = default)
        {
            var discount = await _discountRepository.FindAsync(specification, cancellationToken);
            if (discount == null)
            {
                return Result
                    .Fail(Error<Discount>.NotFound, HttpStatusCode.NotFound);
            }

            if (!discount.IsDeleted)
            {
                return Result
                    .Fail(Error<Discount>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            discount.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<Discount>.Restored);
        }
    }
}
