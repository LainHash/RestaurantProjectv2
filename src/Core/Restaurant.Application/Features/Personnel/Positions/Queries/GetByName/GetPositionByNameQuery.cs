using MediatR;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetByName
{
    public record GetPositionByNameQuery(string Name)
        : IRequest<Result<PositionResponse>>
    {
    }
}
