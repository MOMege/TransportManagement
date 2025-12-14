using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Infrastructure.Persistence.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TableName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.ActionType)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.UserName)
                   .HasMaxLength(100);

            builder.Property(x => x.OldValues)
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.NewValues)
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.ActionDate)
                   .IsRequired();
            
            builder.Property(a => a.ChangedColumns).HasColumnType("nvarchar(max)");
        }
    }
}
