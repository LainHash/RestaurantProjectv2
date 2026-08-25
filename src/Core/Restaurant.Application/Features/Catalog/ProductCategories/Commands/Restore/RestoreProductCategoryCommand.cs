using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Restore
{
    public record RestoreProductCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
