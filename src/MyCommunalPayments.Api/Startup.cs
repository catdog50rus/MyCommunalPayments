using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyCommunalPayments.Api.Infrastucture.ApiServices;
using MyCommunalPayments.BL.Services;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Repositories.Impl;
using MyCommunalPayments.Infrastructure.Mapper;
using System;
using System.IO;
using System.Reflection;

namespace MyCommunalPayments.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Replace with your connection string.
            var connectionString = Configuration.GetConnectionString("MySQLConnection");

            // Replace with your server version and type.
            // Use 'MariaDbServerVersion' for MariaDB.
            // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
            // For common usages, see pull request #1233.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 22));

            // Replace 'YourDbContext' with the name of your own DbContext derived class.
            services.AddDbContext<DBContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                    .EnableDetailedErrors()       // <-- with debugging (remove for production).
            );

            //services.AddScoped<IRepository<Service>, SQLService<Service>>();
            //services.AddScoped<IRepository<Period>, SQLPeriod<Period>>();
            //services.AddScoped<IRepository<Provider>, SQLProvider<Provider>>();
            //services.AddScoped<IRepository<ProvidersServices>, SQLProvidersServices<ProvidersServices>>();
            //services.AddScoped<IRepository<ServiceCounter>, SQLServicesCounter<ServiceCounter>>();
            //services.AddScoped<IRepository<Payment>, SQLPayments<Payment>>();
            //services.AddScoped<IRepository<InvoiceServices>, SQLInvoiceServises<InvoiceServices>>();
            //services.AddScoped<IRepository<Invoice>, SQLInvoice<Invoice>>();

            services.AddScoped<IApiFileService, ApiFileService>();

            services.AddCustomAutoMapper();

            services.AddRepositories();

            services.AddServices();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My Communal Payments"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Comunal Pays V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
