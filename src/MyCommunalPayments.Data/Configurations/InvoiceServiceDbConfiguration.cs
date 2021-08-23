using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class InvoiceServiceDbConfiguration : IEntityTypeConfiguration<InvoiceServiceDb>
    {
        public void Configure(EntityTypeBuilder<InvoiceServiceDb> builder)
        {
            builder.ToTable("InvoceServices").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("IdInvoiceServices");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.IdInvoice);
            builder.Property(p => p.IdService);
            builder.Property(p => p.Amount);
        }
    }
}
