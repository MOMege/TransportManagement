using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Infrastructure.Persistence.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasMany(d => d.trips)
               .WithOne(t => t.Driver)
               .HasForeignKey(t => t.DriverId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}