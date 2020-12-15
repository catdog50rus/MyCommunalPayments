using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace MyCommunalPayments.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            
            services.AddScoped<IRepository<Service>, SQLService<Service>>();
            services.AddScoped<IRepository<Period>, SQLPeriod<Period>>();
            services.AddScoped<IRepository<Provider>, SQLProvider<Provider>>();
            services.AddScoped<IRepository<ProvidersServices>, SQLProvidersServices<ProvidersServices>>();
            services.AddScoped<IRepository<ServiceCounter>, SQLServicesCounter<ServiceCounter>>();
            services.AddScoped<IRepository<Payment>, SQLPayments<Payment>>();
            services.AddScoped<IRepository<InvoiceServices>, SQLInvoiceServises<InvoiceServices>>();
            services.AddScoped<IRepository<Invoice>, SQLInvoice<Invoice>>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
