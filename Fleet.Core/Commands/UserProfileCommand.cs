using System;
using Coravel;
using Fleet.Core.Processes;

namespace Fleet.Core.Commands
{
    public static class UserProfileCommand
    {
        public static void UpdateUserAccountBalance(this IServiceProvider services)
        {
            services.UseScheduler ( scheduler =>
            {
                var job = scheduler.Schedule<UserProfileProcess>();
                job.Hourly();
            } );
        }
    }
}