using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.ExternalServices.Query
{
    
        public record GetVehicleLocationQuery(string PlateNumber)
    : IRequest<Result<VehicleLocationDto>>;

        public class GetVehicleLocationHandler
            : IRequestHandler<GetVehicleLocationQuery, Result<VehicleLocationDto>>
        {
            private readonly IGpsTrackingService _gps;

            public GetVehicleLocationHandler(IGpsTrackingService gps)
            {
                _gps = gps;
            }

            public async Task<Result<VehicleLocationDto>> Handle(
                GetVehicleLocationQuery request,
                CancellationToken cancellationToken)
            {
                var location = await _gps.GetLatestLocationAsync(request.PlateNumber);

                if (location is null)
                    return Result<VehicleLocationDto>.Failure("GPS data not found");

                return Result<VehicleLocationDto>.Success(location, "Location retrieved");
            }
        }
}
