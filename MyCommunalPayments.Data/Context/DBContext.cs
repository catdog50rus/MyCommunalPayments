using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.Data.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<InvoiceServices> InvoiceServices {get; set;}
        public DbSet<ProvidersServices> ProvidersServices { get; set; }
        public DbSet<ServiceCounter> ServiceCounters { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
