using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Delete
{
    public class DeletePositionSpecification
        : BaseSpecification<Position>
    {
        public DeletePositionSpecification(DeletePositionCommand command)
        {
            Criteria = position => position.PublicId == command.Id;
        }
    }
}
