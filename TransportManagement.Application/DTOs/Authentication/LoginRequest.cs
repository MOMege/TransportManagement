using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.DTOs.Authentication
{
    public record LoginRequest
    ( string UserName,
     string Password);
       
    
}
