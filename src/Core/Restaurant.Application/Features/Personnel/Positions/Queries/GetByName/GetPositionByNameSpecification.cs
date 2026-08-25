using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetByName
{
    public class GetPositionByNameSpecification
        : BaseSpecification<Position>
    {
        public GetPositionByNameSpecification(GetPositionByNameQuery query)
        {
            AddCriteria(x => x.Name == query.Name);
            AddInclude(x => x.Department);
        }
    }
}
