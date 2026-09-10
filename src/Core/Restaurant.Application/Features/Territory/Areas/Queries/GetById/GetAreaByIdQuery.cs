using MediatR;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetById
{
    public record GetAreaByIdQuery(Guid Id)
        : IRequest<Result<AreaResponse>>
    {
    }
}
