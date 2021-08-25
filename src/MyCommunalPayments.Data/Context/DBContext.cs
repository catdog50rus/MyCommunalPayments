using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Configurations;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.Data.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        //public DbSet<Service> Services { get; set; }
        //public DbSet<Provider> Providers { get; set; }
        //public DbSet<Invoice> Invoices { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<Transactions> Transactions { get; set; }
        //public DbSet<Period> Periods { get; set; }
        //public DbSet<InvoiceServices> InvoiceServices { get; set; }
        //public DbSet<ProvidersServices> ProvidersServices { get; set; }
        //public DbSet<ServiceCounter> ServiceCounters { get; set; }
        //public DbSet<Order> Orders { get; set; }


        public DbSet<InvoiceDb> Invoices { get; set; }
        public DbSet<PeriodDb> Periods { get; set; }
        public DbSet<ProviderDb> Providers { get; set; }
        public DbSet<InvoiceServiceDb> InvoiceServices { get; set; }
        public DbSet<OrderDb> Orders { get; set; }
        public DbSet<PaymentDb> Payments { get; set; }
        public DbSet<ProviderServiceDb> ProvidersServices { get; set; }
        public DbSet<ServiceCounterDb> ServiceCounters { get; set; }
        public DbSet<ServiceDb> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.ApplyConfiguration(new InvoiceDbConfiguration());
            builder.ApplyConfiguration(new PeriodDbConfiguration());
            builder.ApplyConfiguration(new ProviderDbConfiguration());
            builder.ApplyConfiguration(new InvoiceServiceDbConfiguration());
            builder.ApplyConfiguration(new PaymentDbConfiguration());
            builder.ApplyConfiguration(new OrderDbConfiguration());
            builder.ApplyConfiguration(new ProviderServiceDbConfiguration());
            builder.ApplyConfiguration(new ServiceDbConfiguration());
            builder.ApplyConfiguration(new ServiceCounterDbConfiguration());
            
            base.OnModelCreating(builder);

        }
    }
}
