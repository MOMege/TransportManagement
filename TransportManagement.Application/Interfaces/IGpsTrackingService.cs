using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;

namespace TransportManagement.Application.Interfaces
{
    public interface IGpsTrackingService
    {
        Task<VehicleLocationDto?> GetLatestLocationAsync(string plateNumber);
    }
}
