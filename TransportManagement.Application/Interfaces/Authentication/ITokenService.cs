using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

    }
}


