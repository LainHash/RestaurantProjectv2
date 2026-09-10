using MediatR;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAllByBranchId
{
    public record GetAllAreasByBranchIdQuery(Guid BranchId)
        : IRequest<Result<IEnumerable<AreaResponse>>>
    {
    }
}
