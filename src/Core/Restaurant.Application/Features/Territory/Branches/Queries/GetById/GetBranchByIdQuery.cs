using MediatR;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Branches.Queries.GetById
{
    public record GetBranchByIdQuery(Guid Id)
        : IRequest<Result<BranchDetailResponse>>
    {
    }
}
