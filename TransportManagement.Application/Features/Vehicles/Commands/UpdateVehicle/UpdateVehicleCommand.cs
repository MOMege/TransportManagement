using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(
     Guid Id,
    decimal MaxLoadKg,
    VechileType Type,
    int DoorNumber) : IRequest<bool>;
    
}
