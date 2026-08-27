using AutoMapper;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Pricing;

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
    }
}
