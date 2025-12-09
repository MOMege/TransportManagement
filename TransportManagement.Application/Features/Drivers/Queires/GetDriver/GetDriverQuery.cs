using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Queires.GetDriver
{
    public record GetDriverQuery (Guid id): IRequest<Result<DriverDto>>;
   
}
