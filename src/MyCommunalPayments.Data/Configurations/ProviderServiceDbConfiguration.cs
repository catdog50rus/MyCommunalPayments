using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class ProviderServiceDbConfiguration : IEntityTypeConfiguration<ProviderServiceDb>
    {
        public void Configure(EntityTypeBuilder<ProviderServiceDb> builder)
        {
            builder.ToTable("ProvidersServices").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("Id");
            //builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            //builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.IdProvider);
            builder.Property(p => p.IdService);

            builder
               .HasOne(x => x.Provider)
               .WithOne()
               .HasForeignKey<ProviderServiceDb>(x => x.IdProvider)
               .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(x => x.Service)
               .WithOne()
               .HasForeignKey<ProviderServiceDb>(x => x.IdService)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
