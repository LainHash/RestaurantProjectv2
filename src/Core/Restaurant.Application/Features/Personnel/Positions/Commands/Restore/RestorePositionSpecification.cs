using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Restore
{
    public class RestorePositionSpecification
        : BaseSpecification<Position>
    {
        public RestorePositionSpecification(RestorePositionCommand command)
        {
            Criteria = position => position.PublicId == command.Id;
        }
    }
}
