using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Restore
{
    public record RestoreBrandCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
