using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Users.Queries.GetById
{
    public class GetUserByIdSpecification
        : BaseSpecification<User>
    {
        public GetUserByIdSpecification(GetUserByIdQuery query)
        {
            AddCriteria(x => x.PublicId == query.Id);

            AddInclude(x => x.Role);
            AddInclude(x => x.PersonalProfile!);
            AddInclude(x => x.Customer!);
            AddIncludeAggregator(x => x.Include(u => u.Employee)
                                        .ThenInclude(e => e!.Position)
                                        .ThenInclude(p => p.Department));
            AddIncludeAggregator(x => x.Include(u => u.Employee)
                                        .ThenInclude(e => e!.Branch));
        }
    }
}
