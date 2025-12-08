using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Application.Features.Vehicles.Commands.AddVehicle
{
   

    public class AddVehicleCommand : IRequest<Guid>
    {
        public string PlateNumber { get; set; }
        public VechileType Type { get; set; }
        public int DoorNumber { get; set; }
        public decimal MaxLoadKg { get; set; }
    }
}
