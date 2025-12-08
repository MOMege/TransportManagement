using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.InvoiceNumber)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(i => i.SubTotal)
               .HasPrecision(10, 2);

        builder.Property(i => i.Tax)
               .HasPrecision(10, 2);
    }
}
