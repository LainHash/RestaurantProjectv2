using AutoMapper;
using Restaurant.Application.Extentions;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Infrastructure.Mapping.Inventory
{
    internal class IngredientStockMapping : Profile
    {
        public IngredientStockMapping()
        {
            CreateMap<IngredientStock, IngredientStockResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.Branch.Code))
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.QuantityOnHand.ToStockStatus()));
        }
    }
}
