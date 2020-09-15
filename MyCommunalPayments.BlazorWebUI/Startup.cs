using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.BlazorWebUI.Pages.Providers;
using MyCommunalPayments.Models.Models;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Data.Services.Repositories;

namespace MyCommunalPayments.BlazorWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DBContext>(options =>
            {
                //options.UseSqlite(Configuration.GetConnectionString("MySQLLiteDB"));
                options.UseMySql(Configuration.GetConnectionString("MySQLConnection"), x => x.ServerVersion("8.0.19-mysql"));
            });
            services.AddScoped<IRepository<Service>, SQLService<Service>>();
            services.AddScoped<IRepository<Provider>, SQLProvider<Provider>>();
            services.AddScoped<IRepository<Period>, SQLPeriod<Period>>();
            services.AddScoped<IRepository<Invoice>, SQLInvoice<Invoice>>();
            services.AddScoped<IRepository<InvoiceServices>, SQLInvoiceServises<InvoiceServices>>();
            services.AddScoped<IRepository<ProvidersServices>, SQLProvidersServices<ProvidersServices>>();
            services.AddScoped<IRepository<Period>, SQLPeriod<Period>>();
            services.AddScoped<IRepository<Payment>, SQLPayments<Payment>>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
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
