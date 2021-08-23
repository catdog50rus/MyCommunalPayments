using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class InvoiceDbConfiguration : IEntityTypeConfiguration<InvoiceDb>
    {
        public void Configure(EntityTypeBuilder<InvoiceDb> builder)
        {
            builder.ToTable("Invoices1");

            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasColumnName("IdInvoice1");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt1").HasColumnType(nameof(DateTime));
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.IdPeriod).HasColumnName("IdPeriod");
            builder.Property(p => p.IdProvider).HasColumnName("IdProvider");
            builder.Property(p => p.InvoiceSum).HasColumnName("InvoiceSum");
            builder.Property(p => p.Pay).HasColumnName("Pay");

            builder.HasOne<PeriodDb>(nameof(InvoiceDb.Period));
            builder.HasOne<ProviderDb>(nameof(InvoiceDb.Provider));
        }
    }
}
