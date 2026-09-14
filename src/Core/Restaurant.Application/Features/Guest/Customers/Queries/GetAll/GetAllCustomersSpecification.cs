using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetAll
{
    public class GetAllCustomersSpecification
        : BaseSpecification<Customer>
    {
        public GetAllCustomersSpecification(GetAllCustomersQuery query)
        {
            AddInclude(x => x.AvatarImage!);
        }
    }
}
