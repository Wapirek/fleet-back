using System.Threading.Tasks;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AccountEntity> GetUserByNameAsync( string login );
        bool CheckPasswordAsync( AccountEntity account, string password );
        
    }
}