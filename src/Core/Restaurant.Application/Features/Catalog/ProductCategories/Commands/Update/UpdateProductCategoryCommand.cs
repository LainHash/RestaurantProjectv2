using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update
{
    public record UpdateProductCategoryCommand(Guid Id, UpdateProductCategoryRequest Body)
        : IRequest<Result<ProductCategoryResponse>>
    {
    }
}
