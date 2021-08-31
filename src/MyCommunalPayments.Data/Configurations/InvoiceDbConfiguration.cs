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
            builder.ToTable("Invoices");

            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasColumnName("IdInvoice");
            //builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            //builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.IdPeriod).HasColumnName("IdPeriod");
            builder.Property(p => p.IdProvider).HasColumnName("IdProvider");
            builder.Property(p => p.InvoiceSum).HasColumnName("InvoiceSum");
            builder.Property(p => p.Pay).HasColumnName("Pay");

            builder
                .HasOne(i => i.Period)
                .WithOne()
                .HasForeignKey<InvoiceDb>(x=>x.IdPeriod)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(i => i.Provider)
                .WithOne()
                .HasForeignKey<InvoiceDb>(x=>x.IdProvider)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
