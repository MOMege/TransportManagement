using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Interfaces.Authentication
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        IEnumerable<string>? Roles { get; }
        string? GetClaim(string claimType);
    }
}
