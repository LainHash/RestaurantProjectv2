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
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.PublicId))
                .ForMember(dest => dest.CustomerCode, opt => opt
                    .MapFrom(src => src.Customer != null ? src.Customer.CustomerCode : null))
                .ForMember(dest => dest.EmployeeCode, opt => opt
                    .MapFrom(src => src.Employee != null ? src.Employee.EmployeeCode : null))
                .ForMember(dest => dest.BranchCode, opt => opt
                    .MapFrom(src => src.Branch.BranchCode))
                .ForMember(dest => dest.RestaurantTableId, opt => opt
                    .MapFrom(src => src.RestaurantTable != null ? (Guid?)src.RestaurantTable.PublicId : null))
                .ForMember(dest => dest.TableNumber, opt => opt
                    .MapFrom(src => src.RestaurantTable != null ? src.RestaurantTable.TableNumber : null))
                .ForMember(dest => dest.DeliveryAddress, opt => opt
                    .MapFrom(src => src.DeliveryAddress))
                .ForMember(dest => dest.OrderDetails, opt => opt
                    .MapFrom(src => src.OrderDetails));
        }
    }
}
