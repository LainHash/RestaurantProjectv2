using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Restore
{
    public record RestoreIngredientCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
