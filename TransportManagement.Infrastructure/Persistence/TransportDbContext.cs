using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain;
using TransportManagement.Domain.Entites;
using TransportManagement.Domain.Enums;
using TransportManagement.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace TransportManagement.Infrastructure.Persistence
{
    public class TransportDbContext :IdentityDbContext<ApplicationUser >

    {
        public TransportDbContext( DbContextOptions<TransportDbContext > options
            ) :base(options)
        {

        }
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<AuditLog> AuditLogs=> Set<AuditLog>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransportDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            /// ده تحويل من  dictionary to string 
            /*   var dictionaryConverter = new ValueConverter<Dictionary<string, object>?, string?>(
               v => v == null ? null : JsonConvert.SerializeObject(v),
               v => v == null ? null : JsonConvert.DeserializeObject<Dictionary<string, object>>(v)
               );

            modelBuilder.Entity<AuditLog>()
                .Property(a => a.OldValues)
                .HasConversion(dictionaryConverter);

            modelBuilder.Entity<AuditLog>()
                .Property(a => a.NewValues)
                .HasConversion(dictionaryConverter);*/

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(GetIsDeletedFilter(entityType.ClrType));
                }
            }

            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
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
        IsActive = true,
        IsDeleted = false
    },
    new
    {
        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        PlateNumber = "XYZ999",
        Type = VechileType.Van,
        DoorNumber = 4,
        MaxLoadKg = 800m,
        CreatedAt = DateTime.UtcNow,
        IsActive = true,
        IsDeleted = false
    }
             );
          

        }
        private static LambdaExpression GetIsDeletedFilter(Type type)
        {
            var param = Expression.Parameter(type, "e");
            var prop = Expression.Property(param, nameof(BaseEntity.IsDeleted));
            var condition = Expression.Equal(prop, Expression.Constant(false));
            return Expression.Lambda(condition, param);
        }

    }
}
