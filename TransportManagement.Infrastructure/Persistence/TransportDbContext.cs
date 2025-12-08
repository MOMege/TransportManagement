using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Infrastructure.Persistence
{
    public class TransportDbContext :DbContext
    {
        public TransportDbContext( DbContextOptions<TransportDbContext > options
            ) :base(options)
        {

        }
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Trip> Trips => Set<Trip>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransportDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>().HasKey(v =>v.Id);
            modelBuilder.Entity<Driver>().HasKey(d => d.Id);
            modelBuilder.Entity<Trip>().HasKey(t => t.Id);
            modelBuilder.Entity<Invoice>().HasKey(i => i.Id);

     

            modelBuilder.Entity<Vehicle>().HasData(

    new
    {
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        PlateNumber = "ABC123",
        Type = VechileType.Truck,
        DoorNumber = 2,
        MaxLoadKg = 2000m,
        CreatedAt = DateTime.UtcNow,
        IsActive = true
    },
    new
    {
        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        PlateNumber = "XYZ999",
        Type = VechileType.Van,
        DoorNumber = 4,
        MaxLoadKg = 800m,
        CreatedAt = DateTime.UtcNow,
        IsActive = true
    }
);
          

        }

    }
}
