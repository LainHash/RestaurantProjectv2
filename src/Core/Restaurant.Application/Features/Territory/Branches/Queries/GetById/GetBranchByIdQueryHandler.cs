using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Branches.Queries.GetById
{
    internal class GetBranchByIdQueryHandler(IBranchService branchService)
                : IRequestHandler<GetBranchByIdQuery, Result<BranchDetailResponse>>
    {
        private readonly IBranchService _branchService = branchService;

        public async Task<Result<BranchDetailResponse>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetBranchByIdSpecification(request);
            var response = await _branchService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
