using AutoMapper;
using Restaurant.Contract.DTOs.Sale.OrderDetails;
using Restaurant.Domain.Entities.Sale;

namespace Restaurant.Infrastructure.Mapping.Sale
{
    internal class OrderDetailMapping : Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, OrderDetailResponse>()
                .ForMember(dest => dest.ProductName, opt => opt
                    .MapFrom(src => src.Product.Name));
        }
    }
}
