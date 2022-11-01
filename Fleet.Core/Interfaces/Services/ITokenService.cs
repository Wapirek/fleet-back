using System.Threading.Tasks;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AccountEntity user);
    }
}