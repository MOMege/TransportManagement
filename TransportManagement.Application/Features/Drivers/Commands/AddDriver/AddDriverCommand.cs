using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Commands.AddDriver
{
    public record AddDriverCommand (
        string FullName,
        string PhoneNumber,
        bool IsActive
        ) : IRequest<Result<Guid>>;
    
}
