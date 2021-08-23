using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class PaymentDbConfiguration : IEntityTypeConfiguration<PaymentDb>
    {
        public void Configure(EntityTypeBuilder<PaymentDb> builder)
        {
            builder.ToTable("Payments").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("IdPayment");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.IdInvoice);
            builder.Property(p => p.IdOrder);
            builder.Property(p => p.PaymentSum);
            builder.Property(p => p.Paid);
            builder.Property(p => p.DatePayment);
        }
    }
}
