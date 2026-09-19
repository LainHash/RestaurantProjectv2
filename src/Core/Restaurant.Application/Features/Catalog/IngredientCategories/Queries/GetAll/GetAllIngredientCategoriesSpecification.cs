using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetAll
{
    public class GetAllIngredientCategoriesSpecification
        : BaseSpecification<IngredientCategory>
    {
        public GetAllIngredientCategoriesSpecification(GetAllIngredientCategoriesQuery query)
        {
            EnableSoftDeleteFilter();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.Name);
                    else
                        ApplyOrderByDescending(p => p.Name);
                    break;
                default:
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
