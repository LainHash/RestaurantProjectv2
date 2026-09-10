using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Restore
{
    public record RestoreAreaCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
