using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Fleet.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityService( this IServiceCollection services, IConfiguration config )
        {
            services.AddAuthentication ()
                .AddJwtBearer ( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( config["Token:Key"] ) ),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                } );
            services.ConfigureApplicationCookie ( options =>
            {
                options.LoginPath = "/login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes ( 30 );
            } );
        }
    }
}