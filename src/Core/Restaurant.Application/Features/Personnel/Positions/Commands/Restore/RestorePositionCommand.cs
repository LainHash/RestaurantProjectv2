using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Restore
{
    public record RestorePositionCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
