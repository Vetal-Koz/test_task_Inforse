using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Core.DTO.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Services.Impl
{
    public class JwtServiceImpl : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtServiceImpl(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public AuthResposne CreateJwtToken(ApplicationUser user, IList<string> roles)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.GetSection("Jwt")["EXPIRATION_MINUTES"]));

            List<Claim> claims = new List<Claim> {

                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.NameIdentifier, user.Email),

            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Key"]));

            SigningCredentials signingCredentials = new
                SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthResposne() { Token = token, Email = user.Email, Expiration = expiration, Roles = roles.ToList() };
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            throw new NotImplementedException();
        }
    }
}
