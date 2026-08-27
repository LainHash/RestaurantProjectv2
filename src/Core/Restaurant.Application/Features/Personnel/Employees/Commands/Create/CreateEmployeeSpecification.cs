using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Employees.Commands.Create
{
    public class CreateEmployeeSpecification
        : BaseSpecification<Employee>
    {
        public CreateEmployeeSpecification(CreateEmployeeCommand command)
        {
            AddCriteria(x => x.User.PublicId == command.Body.UserId);

            AddInclude(x => x.Position);
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.Role));
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.PersonalProfile));
        }
    }
}
