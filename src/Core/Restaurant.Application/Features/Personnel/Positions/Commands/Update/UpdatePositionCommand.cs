using MediatR;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Update
{
    public record UpdatePositionCommand(Guid Id, UpdatePositionRequest Body)
        : IRequest<Result<PositionResponse>>
    {
    }
}
