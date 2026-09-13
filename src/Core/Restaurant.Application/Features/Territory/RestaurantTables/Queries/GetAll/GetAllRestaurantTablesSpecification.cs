using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll
{
    public class GetAllRestaurantTablesSpecification
        : BaseSpecification<RestaurantTable>
    {
        public GetAllRestaurantTablesSpecification(GetAllRestaurantTablesQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(rt =>
                    EF.Functions.Like(rt.TableNumber, $"%{query.Keyword}%") ||
                    EF.Functions.Like(nameof(rt.Shape), $"%{query.Keyword}%") ||
                    EF.Functions.Like(nameof(rt.Status), $"%{query.Keyword}%"));
            }

            if (query.AreaId is not null)
            {
                AddCriteria(x => x.Area.PublicId == query.AreaId);
            }

            switch (query.SortField)
            {
                case SortField.Capacity:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.Capacity);
                    else
                        ApplyOrderByDescending(p => p.Capacity);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
