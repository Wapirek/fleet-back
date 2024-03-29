﻿using System;
using Fleet.API.Extensions;
using Fleet.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fleet.Infrastructure.Helpers;

namespace Fleet.API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup( IConfiguration config )
        {
            _config = config;
        }
        
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var connectionString = _config.GetConnectionString ( "FleetConnection" );
            var serverVersion = new MariaDbServerVersion ( new Version ( 10, 4, 20 ) );
            services.AddDbContext<FleetContext> ( x => x.UseMySql (
                connectionString , serverVersion ) );

            services.AddAutoMapper ( typeof(MappingProfiles) );
            services.AddApplicationServices();
            services.AddIdentityService ( _config );
            
            services.AddCors ( opt =>
            {
                opt.AddPolicy ( "CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins ( "*" );
                } );
            } );
        }
        
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            app.UseCors ( "CorsPolicy" );
            
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI ( c => c.SwaggerEndpoint ( "/swagger/v1/swagger.json", "Api v1" ) );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints ( endpoints => { endpoints.MapControllers(); } );
        }
    }
}