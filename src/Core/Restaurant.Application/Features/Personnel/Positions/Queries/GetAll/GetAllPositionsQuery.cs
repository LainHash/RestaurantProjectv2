using MediatR;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetAll
{
    public record GetAllPositionsQuery()
        : IRequest<Result<IEnumerable<PositionResponse>>>
    {
    }
}
