using MediatR;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetById
{
    public record GetPositionByIdQuery(Guid Id)
        : IRequest<Result<PositionResponse>>
    {
    }
}
