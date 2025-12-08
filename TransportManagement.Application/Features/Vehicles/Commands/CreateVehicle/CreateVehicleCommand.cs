using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Application.Features.Vehicles.Commands.CreateVehicle
{
    public record CreateVehicleCommand 
        (
        string PlateNumber,
       VechileType Type,
      int DoorNumber,
     decimal MaxLoadKg) :IRequest<Guid>;
    
}
