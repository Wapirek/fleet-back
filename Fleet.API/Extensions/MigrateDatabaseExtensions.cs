using System.Threading.Tasks;
using Fleet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fleet.API.Extensions
{
    public static class MigrateDatabaseExtensions
    {
        public static async Task MigrateFleet( this FleetContext context )
        {
            await context.Database.MigrateAsync();
        }
    }
}