using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Toast;
using MyCommunalPayments.Data.Services.Upload;
using MyCommunalPayments.Models.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace MyCommunalPayments.BlazorWebUI
{
    public class Startup
    {
        //[SuppressMessage("CodeQuality", "IDE0051:Удалите неиспользуемые закрытые члены", Justification = "<Ожидание>")]
        //private const string _apiPathIIS = @"http://localhost:8080/";
        //[SuppressMessage("CodeQuality", "IDE0051:Удалите неиспользуемые закрытые члены", Justification = "<Ожидание>")]
        private const string _apiPathExpress = @"https://localhost:44390/";

        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDbContextPool<DBContext>(options =>
            {
            //options.UseSqlite(Configuration.GetConnectionString("MySQLiteDB"));
            
            options.UseMySql(Configuration.GetConnectionString("MySQLConnection"),
                    new MySqlServerVersion(new Version(8, 0, 19)), 
                    mySqlOptions => mySqlOptions
                        .CharSetBehavior(CharSetBehavior.NeverAppend))
                    // Everything from this point on is optional but helps with debugging.
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
            });

            //string _apiPath = _apiPathIIS;
            string _apiPath = _apiPathExpress;
            services.AddHttpClient<IApiRepository<Service>, ServicesService>(client=> 
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<Period>, PeriodsService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<Provider>, ProvidersService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<ProvidersServices>, ProviderServicesService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<ServiceCounter>, ServiceCounterService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<Payment>, PaymentsService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<InvoiceServices>, InvoiceServicesService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });
            services.AddHttpClient<IApiRepository<Invoice>, InvoiceService>(client =>
            {
                client.BaseAddress = new Uri(_apiPath);
            });

            services.AddScoped<IFileLoad, SQLFileLoad>();
            services.AddScoped<IToast, ToastService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
