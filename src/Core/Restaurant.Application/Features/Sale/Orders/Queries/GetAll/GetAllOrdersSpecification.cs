using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Sale.Orders.Queries.GetAll
{
    public class GetAllOrdersSpecification
        : BaseSpecification<Order>
    {
        public GetAllOrdersSpecification(GetAllOrdersQuery query)
        {
            AddInclude(x => x.Customer);
            AddInclude(x => x.Employee);
            AddInclude(x => x.Branch);
            AddInclude(x => x.RestaurantTable);

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(nameof(p.Status), $"%{query.Keyword}%") ||
                    EF.Functions.Like(nameof(p.Type), $"%{query.Keyword}%"));
            }

            if (query.CustomerCode is not null)
            {
                AddCriteria(p =>
                    p.Customer.CustomerCode == query.CustomerCode);
            }

            if (query.EmployeeCode is not null)
            {
                AddCriteria(p =>
                    p.Employee.EmployeeCode == query.EmployeeCode);
            }

            if (query.BranchCode is not null)
            {
                AddCriteria(p =>
                    p.Branch.BranchCode == query.BranchCode);
            }

            switch (query.SortField)
            {
                case "price":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.TotalAmount);
                    else
                        ApplyOrderByDescending(p => p.TotalAmount);
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
