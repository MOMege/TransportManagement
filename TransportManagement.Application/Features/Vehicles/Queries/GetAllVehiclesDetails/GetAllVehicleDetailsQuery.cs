using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;

namespace TransportManagement.Application.Features.Vehicles.Queries.GetAllVehiclesDetails
{
    public record GetAllVehicleDetailsQuery : IRequest<IEnumerable< VehicleDto>>;
    
}
