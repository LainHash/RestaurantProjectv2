using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetAllByDeparmentId
{
    public class GetAllPositionByDepartmentIdSpecification
        : BaseSpecification<Position>
    {
        public GetAllPositionByDepartmentIdSpecification(GetAllPositionByDepartmentIdQuery query)
        {
            AddCriteria(x => x.Department.PublicId == query.DepartmentId);
            AddInclude(x => x.Department);
        }
    }
}
