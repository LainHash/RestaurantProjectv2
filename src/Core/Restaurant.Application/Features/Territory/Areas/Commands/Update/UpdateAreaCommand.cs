using MediatR;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Update
{
    public record UpdateAreaCommand(Guid Id, UpdateAreaRequest Body)
        : IRequest<Result<AreaResponse>>
    {
    }
}
