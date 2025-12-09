using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Commands.UpdateDriver
{
    public record UpdateDriverCommand (Guid Id,UpdateDriverDto Dto)
        : IRequest<Result>;
}
