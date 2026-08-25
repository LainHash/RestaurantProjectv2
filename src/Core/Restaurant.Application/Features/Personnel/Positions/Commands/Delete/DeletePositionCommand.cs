using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Delete
{
    public record DeletePositionCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
