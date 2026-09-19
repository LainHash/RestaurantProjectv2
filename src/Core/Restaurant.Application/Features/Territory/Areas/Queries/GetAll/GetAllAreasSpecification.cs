using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    public class GetAllAreasSpecification
        : BaseSpecification<Area>
    {
        public GetAllAreasSpecification(GetAllAreasQuery query)
        {
            EnableSoftDeleteFilter();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(a =>
                    EF.Functions.Like(a.Name, $"%{query.Keyword}%") ||
                    (a.Description != null && EF.Functions.Like(a.Description, $"%{query.Keyword}%")));
            }

            if (!string.IsNullOrWhiteSpace(query.BranchCode))
            {
                AddCriteria(a => a.Branch.BranchCode == query.BranchCode);
            }

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyOrderBy(a => a.Name);
                    else
                        ApplyOrderByDescending(a => a.Name);
                    break;
                case "display_order":
                    if (query.IsAscending)
                        ApplyOrderBy(a => a.DisplayOrder);
                    else
                        ApplyOrderByDescending(a => a.DisplayOrder);
                    break;
                default:
                    if (query.IsAscending)
                        ApplyOrderBy(a => a.CreatedAt);
                    else
                        ApplyOrderByDescending(a => a.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
