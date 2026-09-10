using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Delete
{
    public class DeleteAreaSpecification
        : BaseSpecification<Area>
    {
        public DeleteAreaSpecification(DeleteAreaCommand command)
        {
            AddCriteria(x => x.PublicId == command.Id);
        }
    }
}
