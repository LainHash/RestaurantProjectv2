using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAllByBranchId
{
    public class GetAllAreasByBranchIdSpecification
        : BaseSpecification<Area>
    {
        public GetAllAreasByBranchIdSpecification(GetAllAreasByBranchIdQuery query)
        {
            AddCriteria(x => x.Branch.PublicId == query.BranchId);

            AddInclude(x => x.Branch);
        }
    }
}
