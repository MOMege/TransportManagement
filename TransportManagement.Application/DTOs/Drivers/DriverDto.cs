using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.DTOs.Driveres
{
    public record DriverDto

    (  Guid Id,
     string? FullName,
     string? PhoneNumber,
     bool IsActive);
    
    
}
