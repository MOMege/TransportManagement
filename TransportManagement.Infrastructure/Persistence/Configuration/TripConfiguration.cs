using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Infrastructure.Persistence.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TripNumber)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(t => t.Origin)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Destination)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.LoadWeightKg)
               .HasPrecision(10, 2);

        builder.Property(t => t.BasePrice)
               .HasPrecision(10, 2);
    }
}