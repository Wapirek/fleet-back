using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using Fleet.Core.Interfaces.Services;

namespace Fleet.Core.Processes
{
    public class UserProfileProcess : IInvocable
    {
        private readonly IUserProfileService _profileService;

        public UserProfileProcess(IUserProfileService profileService)
        {
            _profileService = profileService;
        }
        
        public async Task Invoke()
        {
           await _profileService.UpdateAccountBalanceAsync();
        }
    }
}