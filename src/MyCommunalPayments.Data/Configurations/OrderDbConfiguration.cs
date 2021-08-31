using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<OrderDb>
    {
        public void Configure(EntityTypeBuilder<OrderDb> builder)
        {
            builder.ToTable("Orders").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("IdOrder");
            //builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            //builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.FileName);
            builder.Property(p => p.OrderScreen);

        }
    }
}
