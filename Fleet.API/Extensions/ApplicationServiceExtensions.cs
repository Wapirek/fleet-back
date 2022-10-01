using Fleet.Core.Interfaces.Repositories;
using Fleet.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices( this IServiceCollection services )
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}