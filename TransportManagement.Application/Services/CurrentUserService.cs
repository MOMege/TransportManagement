using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces.Authentication;

namespace TransportManagement.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService( IHttpContextAccessor httpContextAccessor) { 
        _httpContextAccessor = httpContextAccessor;
        }
        public string? UserId => _httpContextAccessor?.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName => _httpContextAccessor?.HttpContext?.User?
            .Identity?.Name;

        public string? Email => _httpContextAccessor?.HttpContext?.User?
            .FindFirstValue(ClaimTypes.Email);

        public IEnumerable<string>? Roles => _httpContextAccessor?.HttpContext?.User?
            .FindAll(ClaimTypes.Role)?.Select(r => r.Value);

        public string? GetClaim(string claimType)=> _httpContextAccessor.HttpContext?.User?
            .FindFirst(claimType)?.Value;
      
    }
}
