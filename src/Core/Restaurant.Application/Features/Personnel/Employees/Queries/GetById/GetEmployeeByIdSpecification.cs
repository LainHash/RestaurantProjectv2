using Microsoft.EntityFrameworkCore;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Employees.Queries.GetById
{
    public class GetEmployeeByIdSpecification
        : BaseSpecification<Employee>
    {
        public GetEmployeeByIdSpecification(GetEmployeeByIdQuery query)
        {
            AddInclude(x => x.AvatarImage!);
            AddInclude(x => x.Position);
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.Role));
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.PersonalProfile));

            AddCriteria(x => x.PublicId == query.Id);
        }
    }
}
