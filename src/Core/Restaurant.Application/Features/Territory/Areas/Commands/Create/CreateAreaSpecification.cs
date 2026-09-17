using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Create
{
    public class CreateAreaSpecification
        : BaseSpecification<Area>
    {
        public CreateAreaSpecification()
        {
            AddInclude(x => x.Branch);
        }

        public void ApplyCriteria(long id)
        {
            AddCriteria(x => x.Id == id);
        }
    }
}
