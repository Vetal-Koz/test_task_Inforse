using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Core.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Services
{
    public interface IJwtService
    {
        AuthResposne CreateJwtToken(ApplicationUser user, IList<string> roles);
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
