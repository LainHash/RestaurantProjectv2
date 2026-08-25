using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Delete
{
    public record DeleteProductCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
