using Restaurant.Application.Features.Territory.Branches.Queries.GetAll;
using Restaurant.Application.Features.Territory.Branches.Queries.GetById;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Territory
{
    public interface IBranchService
    {
        Task<PageResult<IEnumerable<BranchResponse>>> GetAllAsync(
            GetAllBranchesSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<BranchDetailResponse>> GetByIdAsync(
            GetBranchByIdSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
