using AutoMapper;
using Restaurant.Contract.DTOs.Billing.InvoiceDetails;
using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Infrastructure.Mapping.Billing
{
    internal class InvoiceDetailMapping : Profile
    {
        public InvoiceDetailMapping()
        {
            CreateMap<InvoiceDetail, InvoiceDetailResponse>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.PublicId));
        }
    }
}
