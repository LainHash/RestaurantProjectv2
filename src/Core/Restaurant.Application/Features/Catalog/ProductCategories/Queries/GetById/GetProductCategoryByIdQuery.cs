using MediatR;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetById
{
    public record GetProductCategoryByIdQuery(Guid Id) 
        : IRequest<Result<ProductCategoryResponse>>
    {
    }
}
