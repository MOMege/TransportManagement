using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IVehicleRepository Vehicles { get; }
        ITripRepository Trips { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<Driver> Drivers { get; }
        IRepository<AuditLog> AuditLogs {  get; }
        Task<int> SaveChangesAsync();
    }
}
