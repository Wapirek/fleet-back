using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fleet.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( _config["Token:Key"] ) );
        }
        
        public async Task<string?> CreateToken( AccountEntity user )
        {
            var claims = new List<Claim>
            {
                new Claim ( ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim ( ClaimTypes.Name, user.Username )
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var creds = new SigningCredentials ( _key, SecurityAlgorithms.HmacSha512Signature );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"],
                Expires = DateTime.Now.AddDays(30),
                
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken ( tokenDescriptor );
            var createdToken = tokenHandler.WriteToken ( token );

            
            return createdToken;
        }
    }
}