using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Mapping.Catalog
{
    internal class IngredientMapping : Profile
    {
        public IngredientMapping()
        {
            CreateMap<Ingredient, IngredientResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.IngredientPrice.UnitPrice))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.IngredientPrice.Currency))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.IngredientCategory.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.BaseUnit.Symbol));

            CreateMap<CreateIngredientRequest, Ingredient>()
                .ForPath(dest => dest.IngredientPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.IngredientPrice.Currency, opt => opt.MapFrom(src => src.Currency));

            CreateMap<UpdateIngredientRequest, Ingredient>()
                .ForPath(dest => dest.IngredientPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.IngredientPrice.Currency, opt => opt.MapFrom(src => src.Currency));
        }
    }
}
