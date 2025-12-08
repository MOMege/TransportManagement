using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Features.Drivers.Queires.GetAllDrivers
{
    public record GetAllDriverQuery : IRequest<Result< IEnumerable<DriverDto>>>;
   
}
