using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Users.Queries.GetAll
{
    public class GetAllUsersSpecification
        : BaseSpecification<User>
    {
        public GetAllUsersSpecification(GetAllUsersQuery query)
        {
            AddInclude(x => x.Role);

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.UserName, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Email, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Role.Name, $"%{query.Keyword}%"));
            }

            if(query.RoleId is not null)
            {
                AddCriteria(x => x.Role.PublicId == query.RoleId);
            }

            switch (query.SortField)
            {
                case SortField.CreatedAt:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
                case SortField.Name:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.UserName);
                    else
                        ApplyOrderByDescending(p => p.UserName);
                    break;
            }


            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
