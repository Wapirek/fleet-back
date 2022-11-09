using Coravel;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Processes;
using Fleet.Infrastructure.Repositories;
using Fleet.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices( this IServiceCollection services )
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScheduler();
            services.AddTransient<UserProfileProcess>();
        }
    }
}