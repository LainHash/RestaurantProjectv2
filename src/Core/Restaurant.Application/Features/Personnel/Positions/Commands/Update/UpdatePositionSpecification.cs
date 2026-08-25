using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Update
{
    public class UpdatePositionSpecification
        : BaseSpecification<Position>
    {
        public UpdatePositionSpecification(UpdatePositionCommand command)
        {
            Criteria = p => p.PublicId == command.Id;
        }
    }
}
