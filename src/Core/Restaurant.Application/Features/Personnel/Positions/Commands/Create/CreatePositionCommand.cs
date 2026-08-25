using MediatR;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Create
{
    public record CreatePositionCommand(CreatePositionRequest Body)
        : IRequest<Result<PositionResponse>>
    {
    }
}
