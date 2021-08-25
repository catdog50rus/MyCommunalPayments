using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class ServiceCounterDbConfiguration : IEntityTypeConfiguration<ServiceCounterDb>
    {
        public void Configure(EntityTypeBuilder<ServiceCounterDb> builder)
        {
            builder.ToTable("ServicesCounters").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("IdCounter");
            //builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            //builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.DateCount);
            builder.Property(p => p.IdService);
            builder.Property(p => p.ValueCounter);

            builder
               .HasOne(x => x.Service)
               .WithOne()
               .HasForeignKey<ServiceCounterDb>(x => x.IdService)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
