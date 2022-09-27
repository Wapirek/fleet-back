using System;
using Fleet.API;
using Fleet.API.Extensions;
using Fleet.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//var builder = WebApplication.CreateBuilder ( args );
var host = Host.CreateDefaultBuilder ( args )
    .ConfigureWebHostDefaults ( webBuilder => { webBuilder.UseStartup<Startup>(); } )
    .Build();

using ( var scope = host.Services.CreateScope() )
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<FleetContext>();
        await context.MigrateFleet();
    }
    catch ( Exception ex )
    {
        
    }
}
    

await host.RunAsync (  );