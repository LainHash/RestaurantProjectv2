using MediatR;
using Restaurant.Contract.DTOs.General;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.General.Homes.Queries.Get
{
    public record GetHomeQuery(int ProductLimit = 10)
        : IRequest<Result<HomeResponse>>
    {
    }
}
