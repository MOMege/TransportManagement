using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;

namespace TransportManagement.Application.Features.Vehicles.Queries.GetVehicle
{
    public record GetVehicleQuery(Guid id) :IRequest<VehicleDto>;
}
