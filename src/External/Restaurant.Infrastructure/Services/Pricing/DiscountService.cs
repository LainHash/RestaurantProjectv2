using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Claim;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Create;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Update;
using Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Pricing;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Pricing
{
    internal class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountCustomerRepository _discountCustomerRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(
            IDiscountRepository discountRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICustomerRepository customerRepository,
            IDiscountCustomerRepository discountCustomerRepository)
        {
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _discountCustomerRepository = discountCustomerRepository;
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
                Success.Retrieved("Discount"),
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
                .Succeed(response, Success.Created("Discount"), HttpStatusCode.Created);
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
                    .Fail(Error.NotFound("Discount"), HttpStatusCode.NotFound);
            }

            _mapper.Map(command.Body, discount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DiscountResponse>(discount);
            return Result<DiscountResponse>
                .Succeed(response, Success.Updated("Discount"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Discount> specification,
            CancellationToken cancellationToken = default)
        {
            var discount = await _discountRepository.FindAsync(specification, cancellationToken);
            if (discount == null)
            {
                return Result
                    .Fail(Error.NotFound("Discount"), HttpStatusCode.NotFound);
            }

            if (discount.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Discount"), HttpStatusCode.BadRequest);
            }

            discount.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Discount"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Discount> specification,
            CancellationToken cancellationToken = default)
        {
            var discount = await _discountRepository.FindAsync(specification, cancellationToken);
            if (discount == null)
            {
                return Result
                    .Fail(Error.NotFound("Discount"), HttpStatusCode.NotFound);
            }

            if (!discount.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Discount"), HttpStatusCode.BadRequest);
            }

            discount.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Discount"));
        }

        public async Task<Result> ClaimAsync(
            ClaimDiscountCommand command,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.FindByUserIdAsync(command.UserId, cancellationToken);
            if (customer is null)
            {
                return Result
                    .Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
            }

            var discount = await _discountRepository.FindByCodeAsync(command.Body.DiscountCode, cancellationToken);
            if (discount is null)
            {
                return Result
                    .Fail(Error.NotFound("Discount"), HttpStatusCode.NotFound);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var reserved = await _discountRepository.ReserveRemainingQuantityAsync(discount.Id, cancellationToken);
                if (reserved == 0)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result
                        .Fail("This discount is no longer available.", HttpStatusCode.Conflict);
                }

                var discountCustomer = DiscountCustomer.Claim(customer.Id, discount.Id, discount.EndAt);
                _discountCustomerRepository.Add(discountCustomer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result
                    .Succeed("Discount claimed successfully.");
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result
                    .Fail("Already claimed this discount.", HttpStatusCode.Conflict);
            }
        }
    }
}
