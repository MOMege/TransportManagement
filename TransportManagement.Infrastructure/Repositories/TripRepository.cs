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
    public class TripRepository : Repository<Trip> ,ITripRepository
    {
        public TripRepository(TransportDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Trip>> GetTodayTripsAsync()
        {
            var today = DateTime.UtcNow;
            return await _dbset.Where(t=>t.CreatedAt==today).ToListAsync();

        }
           

        public async Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(Guid vehicleId)
        {
        return await  _dbset.Where(t=>t.VehicleId==vehicleId).ToListAsync();     
                }
    }
}
