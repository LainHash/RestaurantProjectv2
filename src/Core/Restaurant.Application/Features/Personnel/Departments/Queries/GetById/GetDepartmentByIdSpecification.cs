using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetById
{
    public class GetDepartmentByIdSpecification
        : BaseSpecification<Department>
    {
        public GetDepartmentByIdSpecification(GetDepartmentByIdQuery query)
        {
            Criteria = department => department.PublicId == query.Id;

            EnableSoftDeleteFilter();
        }
    }
}
