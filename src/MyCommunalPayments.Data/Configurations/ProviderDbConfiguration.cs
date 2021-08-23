using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class ProviderDbConfiguration : IEntityTypeConfiguration<ProviderDb>
    {
        public void Configure(EntityTypeBuilder<ProviderDb> builder)
        {
            builder.ToTable("Providers").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("IdProvider");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.NameProvider);
            builder.Property(p => p.WebSite);

        }
    }
}
