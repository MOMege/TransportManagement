using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        // Primary Key
        builder.HasKey(v => v.Id);

        // Properties
        builder.Property(v => v.PlateNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(v => v.Type)
               .IsRequired();

        builder.Property(v => v.MaxLoadKg)
               .IsRequired()
               .HasPrecision(10, 2);
        builder.Property(v => v.DoorNumber)
            .IsRequired(true)
            .HasMaxLength(9999);


        // Relationships (Vehicle 1 ---- * Trips)
        builder.HasMany(v => v.Trips)
               .WithOne(t => t.Vehicle)
               .HasForeignKey(t => t.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
