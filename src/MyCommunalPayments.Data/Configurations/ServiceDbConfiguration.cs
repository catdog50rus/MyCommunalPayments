using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;

namespace MyCommunalPayments.Data.Configurations
{
    public class ServiceDbConfiguration : IEntityTypeConfiguration<ServiceDb>
    {
        public void Configure(EntityTypeBuilder<ServiceDb> builder)
        {
            builder.ToTable("Services").HasKey(c => c.Id);

            builder.Property(p => p.Id).HasColumnName("IdService");
            //builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            //builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.NameService);
            builder.Property(p => p.IsCounter);
        }
    }
}
