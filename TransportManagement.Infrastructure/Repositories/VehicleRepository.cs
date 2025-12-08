using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;
using TransportManagement.Infrastructure.Persistence;

namespace TransportManagement.Infrastructure.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransportDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
       => await _dbset.Where(v => v.IsActive).ToListAsync();
    }
}
