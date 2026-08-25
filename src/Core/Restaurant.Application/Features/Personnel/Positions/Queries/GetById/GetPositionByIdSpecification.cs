using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetById
{
    public class GetPositionByIdSpecification
        : BaseSpecification<Position>
    {
        public GetPositionByIdSpecification(GetPositionByIdQuery query)
        {
            AddCriteria(x => x.PublicId == query.Id);
            AddInclude(x => x.Department);
        }
    }
}
