using AutoMapper;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Entities.Sale;

namespace Restaurant.Infrastructure.Mapping.Sale
{
    internal class OrderMapping :Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.CustomerCode, opt => opt
                    .MapFrom(src => src.Customer.CustomerCode))
                .ForMember(dest => dest.EmployeeCode, opt => opt
                    .MapFrom(src => src.Employee.EmployeeCode))
                .ForMember(dest => dest.BranchCode, opt => opt
                    .MapFrom(src => src.Branch.Code))
                .ForMember(dest => dest.OrderDetails, opt => opt
                    .MapFrom(src => src.OrderDetails));
        }
    }
}
