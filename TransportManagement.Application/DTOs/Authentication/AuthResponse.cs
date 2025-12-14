using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.DTOs.Authentication
{
    record class AuthResponse
        (
        string Token,
        string UserName,
        string? Role,
        DateTime ExpiresAt
        );
   
}
