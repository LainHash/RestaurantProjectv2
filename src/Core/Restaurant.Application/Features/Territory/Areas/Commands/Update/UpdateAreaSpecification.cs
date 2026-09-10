using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Update
{
    public class UpdateAreaSpecification
        : BaseSpecification<Area>
    {
        public UpdateAreaSpecification(UpdateAreaCommand command)
        {
            AddCriteria(x => x.PublicId == command.Id);
            AddInclude(x => x.Branch);
        }
    }
}
