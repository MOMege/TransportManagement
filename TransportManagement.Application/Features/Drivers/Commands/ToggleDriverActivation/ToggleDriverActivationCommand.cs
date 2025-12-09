using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Commands.ToggleDriverActivation
{
    public record ToggleDriverActivationCommand (Guid Id) : IRequest<Result>;
    
}
