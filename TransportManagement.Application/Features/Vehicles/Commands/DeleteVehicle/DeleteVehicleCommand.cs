using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Vehicles.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand (Guid id): IRequest<Unit>;
}
