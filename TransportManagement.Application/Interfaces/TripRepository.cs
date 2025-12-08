using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Interfaces
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(Guid vehicleId);
        Task<IEnumerable<Trip>> GetTodayTripsAsync();
    }
}
