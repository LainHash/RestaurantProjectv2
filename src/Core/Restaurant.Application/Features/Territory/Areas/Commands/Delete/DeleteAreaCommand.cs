using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Delete
{
    public record DeleteAreaCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
