using AutoMapper;
using Restaurant.Contract.DTOs.Billing.Invoices;
using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Infrastructure.Mapping.Billing
{
    internal class InvoiceMapping : Profile
    {
        public InvoiceMapping()
        {
            CreateMap<Invoice, InvoiceResponse>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.PublicId))
                .ForMember(dest => dest.OrderCode, opt => opt
                    .MapFrom(src => src.Order.OrderCode))
                .ForMember(dest => dest.InvoiceDetails, opt => opt
                    .MapFrom(src => src.InvoiceDetails));

        }
    }
}
