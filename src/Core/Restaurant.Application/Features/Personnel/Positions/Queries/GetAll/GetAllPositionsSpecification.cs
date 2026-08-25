using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetAll
{
    public class GetAllPositionsSpecification
        : BaseSpecification<Position>
    {
        public GetAllPositionsSpecification(GetAllPositionsQuery query)
        {
            AddInclude(x => x.Department);
        }
    }
}
