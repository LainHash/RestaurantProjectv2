using MediatR;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Update
{
    public record UpdateBrandCommand(Guid Id, UpdateBrandRequest Body)
        : IRequest<Result<BrandResponse>>
    {
    }
}
