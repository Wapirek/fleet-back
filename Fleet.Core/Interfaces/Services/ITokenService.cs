using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AccountEntity user);
    }
}