using AutoMapper;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.Infrastructure.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Entity => Model

            CreateMap<InvoiceDb, Invoice>()
                .ForMember(dist=>dist.IdInvoice,
                opt=>opt.MapFrom(i=>i.Id));

            CreateMap<InvoiceServiceDb, InvoiceServices>()
               .ForMember(dist => dist.IdInvoiceServices,
               opt => opt.MapFrom(i => i.Id));

            CreateMap<OrderDb, Order>()
               .ForMember(dist => dist.IdOrder,
               opt => opt.MapFrom(i => i.Id));

            CreateMap<PaymentDb, Payment>()
               .ForMember(dist => dist.IdPayment,
               opt => opt.MapFrom(i => i.Id));

            CreateMap<PeriodDb, Period>()
               .ForMember(dist => dist.IdKey,
               opt => opt.MapFrom(i => i.Id));

            CreateMap<ProviderDb, Provider>()
               .ForMember(dist => dist.IdProvider,
               opt => opt.MapFrom(i => i.Id));

            CreateMap<ProviderServiceDb, ProvidersServices>();

            CreateMap<ServiceDb, Service>()
               .ForMember(dist => dist.IdService,
               opt => opt.MapFrom(i => i.Id));

            CreateMap<ServiceCounterDb, ServiceCounter>()
               .ForMember(dist => dist.IdCounter,
               opt => opt.MapFrom(i => i.Id));

            #endregion


            #region Model => Entity

            CreateMap<Invoice, InvoiceDb>()
                .ForMember(dist => dist.Id,
                opt => opt.MapFrom(i => i.IdInvoice));

            CreateMap<InvoiceServices, InvoiceServiceDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdInvoiceServices));

            CreateMap<Order, OrderDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdOrder));

            CreateMap<Payment, PaymentDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdPayment));

            CreateMap<Period, PeriodDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdKey));

            CreateMap<Provider, ProviderDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdProvider));

            CreateMap<ProvidersServices, ProviderServiceDb>();

            CreateMap<Service, ServiceDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdService));

            CreateMap<ServiceCounter, ServiceCounterDb>()
               .ForMember(dist => dist.Id,
               opt => opt.MapFrom(i => i.IdCounter));

            #endregion
        }
    }
}
