using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Branches.Queries.GetById
{
    public class GetBranchByIdSpecification
        : BaseSpecification<Branch>
    {
        public GetBranchByIdSpecification(GetBranchByIdQuery query)
        {
            AddCriteria(x => x.PublicId == query.Id);

            AddInclude(x => x.Areas);
        }
    }
}
