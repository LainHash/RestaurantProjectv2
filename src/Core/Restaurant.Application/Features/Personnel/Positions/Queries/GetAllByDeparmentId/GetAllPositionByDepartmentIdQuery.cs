using MediatR;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetAllByDeparmentId
{
    public record GetAllPositionByDepartmentIdQuery(Guid DepartmentId)
        : IRequest<Result<IEnumerable<PositionResponse>>>
    {
    }
}
