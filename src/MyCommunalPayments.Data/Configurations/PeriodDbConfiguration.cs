using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCommunalPayments.Data.DBModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Configurations
{
    public class PeriodDbConfiguration : IEntityTypeConfiguration<PeriodDb>
    {
        public void Configure(EntityTypeBuilder<PeriodDb> builder)
        {
            builder.ToTable("Periods1");

            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasColumnName("IdKey");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType(nameof(DateTime));
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType(nameof(DateTime));

            builder.Property(p => p.Year);
            builder.Property(p => p.Month);
        }
    }
}
