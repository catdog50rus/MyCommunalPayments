using Microsoft.Extensions.DependencyInjection;
using MyCommunalPayments.BL.Interfaces;

namespace MyCommunalPayments.BL.Services
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services) => services
            .AddTransient<IInvoiceService, InvoiceService>()
            .AddTransient<IInvoiceServiceService, InvoiceServiceService>()
            .AddTransient<IPaymentService, PaymentService>()
            .AddTransient<IPeriodService, PeriodService>()
            .AddTransient<IProviderServiceService, ProviderServiceService>()
            .AddTransient<IProviderService, ProviderService>()
            .AddTransient<IServiceCounterService, ServiceCounterService>()
            .AddTransient<IServiceService, ServiceService>()
            .AddTransient<IOrderService, OrderService>()
        ;
    }
}
