using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
        
        public string CreateToken( AccountEntity user )
        {
            var claims = new List<Claim>
            {
                new Claim ( ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim ( ClaimTypes.Name, user.Username )
            };

            var creds = new SigningCredentials ( _key, SecurityAlgorithms.HmacSha512Signature );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity ( claims ),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"],
                Expires = DateTime.Now.AddDays(30),
                
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken ( tokenDescriptor );
            
            return tokenHandler.WriteToken ( token );
        }
    }
}