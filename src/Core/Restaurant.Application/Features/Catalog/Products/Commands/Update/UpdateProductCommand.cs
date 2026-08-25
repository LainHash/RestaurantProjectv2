using MediatR;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, UpdateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
