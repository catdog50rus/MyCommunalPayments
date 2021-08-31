using System;
using Microsoft.Extensions.DependencyInjection;
using MyCommunalPayments.Models.Models;
using MyCommunalPayments.UI.ApiServices.Interfaces;

namespace MyCommunalPayments.UI.ApiServices.Registration
{
    public static class ApiServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services, string apiPath)
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddHttpClient<IApiRepository<Service>, ServicesService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<Period>, PeriodsService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<Provider>, ProvidersService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<ProvidersServices>, ProviderServicesService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<ServiceCounter>, ServiceCounterService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<Payment>, PaymentsService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<InvoiceServices>, InvoiceServicesService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IApiRepository<Invoice>, InvoiceService>(client =>
            {
                client.BaseAddress = new Uri(apiPath);
            });
            services.AddHttpClient<IFileService, FileService>(client => { client.BaseAddress = new Uri(apiPath); });
        }
    }
}
