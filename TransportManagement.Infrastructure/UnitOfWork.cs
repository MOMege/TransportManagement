using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;
using TransportManagement.Infrastructure.Persistence;
using TransportManagement.Infrastructure.Repositories;
using System.Text.Json;

public class UnitOfWork : IUnitOfWork
{
    protected readonly TransportDbContext _dbcontext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IVehicleRepository Vehicles { get; }
    public ITripRepository Trips { get; }
    public IRepository<AuditLog> AuditLogs { get; }
    public IRepository<Invoice> Invoices { get; }
    public IRepository<Driver> Drivers { get; }

    public UnitOfWork(TransportDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbcontext = dbContext;
        _httpContextAccessor = httpContextAccessor;

        Vehicles = new VehicleRepository(_dbcontext); // have special repository
        Trips = new TripRepository(_dbcontext);
        Invoices = new Repository<Invoice>(_dbcontext);
        AuditLogs = new Repository<AuditLog>(_dbcontext);
        Drivers = new Repository<Driver>(_dbcontext);
    }

    public async Task<int> SaveChangesAsync()
    {
        // نسجل الـ Audit قبل الحفظ
        AddAuditLogs();
        return await _dbcontext.SaveChangesAsync();
    }

    private void AddAuditLogs()
    {
        var userName = _httpContextAccessor.HttpContext?.User?
                           .FindFirst(ClaimTypes.Name)?.Value
                       ?? "Anonymous";

        var entries = _dbcontext.ChangeTracker.Entries()
            .Where(e => e.Entity is not AuditLog &&
                        e.State != EntityState.Detached &&
                        e.State != EntityState.Unchanged)
            .ToList();

        foreach (var entry in entries)
        {
            // نحاول نجيب الـ Id من Current أو Original
            var idProperty = entry.Property("Id");
            var recordIdObj = idProperty.CurrentValue ?? idProperty.OriginalValue;
            var recordId = recordIdObj is Guid g ? g : Guid.Empty;

            var audit = new AuditLog
            {
                TableName = entry.Metadata.GetTableName() ?? entry.Metadata.ClrType.Name,
                RecordId = recordId,
                ActionType = entry.State.ToString(),  // Added / Modified / Deleted
                UserName = userName,
                ActionDate = DateTime.UtcNow,
            };

            if (entry.State == EntityState.Added)
            {
                audit.NewValues = System.Text.Json.JsonSerializer.Serialize(
                    entry.CurrentValues.Properties
                        .ToDictionary(p => p.Name,
                                      p => entry.CurrentValues[p.Name])
                );
            }
            else if (entry.State == EntityState.Modified)
            {
                audit.OldValues = System.Text.Json.JsonSerializer.Serialize(
                    entry.OriginalValues.Properties
                        .ToDictionary(p => p.Name,
                                      p => entry.OriginalValues[p.Name])
                );

                audit.NewValues = System.Text.Json.JsonSerializer.Serialize(
                    entry.CurrentValues.Properties
                        .ToDictionary(p => p.Name,
                                      p => entry.CurrentValues[p.Name])
                );
            }
            else if (entry.State == EntityState.Deleted)
            {
                audit.OldValues = System.Text.Json.JsonSerializer.Serialize(
                    entry.OriginalValues.Properties
                        .ToDictionary(p => p.Name,
                                      p => entry.OriginalValues[p.Name]
                                     )
                );
            }


            _dbcontext.AuditLogs.Add(audit); // مش محتاج async هنا
        }
    }

    public void Dispose()
    {
        _dbcontext.Dispose();
    }
}
