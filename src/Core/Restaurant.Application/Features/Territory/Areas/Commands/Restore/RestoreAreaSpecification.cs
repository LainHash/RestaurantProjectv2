using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Restore
{
    public class RestoreAreaSpecification
        : BaseSpecification<Area>
    {
        public RestoreAreaSpecification(RestoreAreaCommand command)
        {
            AddCriteria(x => x.PublicId == command.Id);
        }
    }
}
