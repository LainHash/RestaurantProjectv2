using MediatR;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Create
{
    public record CreateAreaCommand(CreateAreaRequest Body)
        : IRequest<Result<AreaResponse>>
    {
    }
}
