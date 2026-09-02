using AutoMapper;
using Restaurant.Application.Extentions;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Infrastructure.Mapping.Inventory
{
    internal class ProductStockMapping : Profile
    {
        public ProductStockMapping()
        {
            CreateMap<ProductStock, ProductStockResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.Branch.BranchCode))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.QuantityOnHand.ToStockStatus()));
        }
    }
}
