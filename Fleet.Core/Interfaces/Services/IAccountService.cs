using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Dtos.Responser;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AccountEntity> GetUserByNameAsync( string login );
        bool CheckPasswordAsync( AccountEntity account, string password );
        Task<ApiResponse<LoginResultDto>> CreateUser( RegisterDto registerDto );
    }
}