using Microsoft.Extensions.DependencyInjection;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL;

namespace MyCommunalPayments.Data.Repositories.Impl
{
    public static class RepositoryRegistrator
    {
        public static void AddRepositories(this IServiceCollection services) => services
            .AddTransient<IInvoiceRepository, InvoiceRepository>()
            .AddTransient<IInvoiceServiceRepository, InvoiceServiceRepository>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IPaymentRepository, PaymentRepository>()
            .AddTransient<IPeriodRepository, PeriodRepository>()
            .AddTransient<IProviderRepository, ProviderRepository>()
            .AddTransient<IProviderServiceRepository, ProviderServiceRepository>()
            .AddTransient<IServiceCounterRepository, ServiceCounterRepository>()
            .AddTransient<IServiceRepository, ServiceRepository>()
        ;
    }
}
