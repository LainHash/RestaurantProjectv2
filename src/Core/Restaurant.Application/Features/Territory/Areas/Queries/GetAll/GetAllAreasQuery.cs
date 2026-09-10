using MediatR;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    public record GetAllAreasQuery
        : PageQuery, IRequest<PageResult<IEnumerable<AreaResponse>>>
    {
        public string? BranchCode { get; init; }
    }
}
