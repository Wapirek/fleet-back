using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Fleet.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityService( this IServiceCollection services, IConfiguration config )
        {

            services.AddAuthentication ( options => 
                {
                    options.DefaultScheme = "JWT_OR_COOKIE";
                    options.DefaultChallengeScheme = "JWT_OR_COOKIE";
                } )
                .AddCookie ( "Cookies", options =>
                {
                    options.LoginPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromDays ( 1 );
                } )
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
                } ).AddPolicyScheme ( "JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        string authorization = context.Request.Headers[HeaderNames.Authorization];
                        if( !string.IsNullOrEmpty ( authorization ) && authorization.StartsWith ( "Bearer " ) )
                            return JwtBearerDefaults.AuthenticationScheme;

                        return CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                } );
        }
    }
}