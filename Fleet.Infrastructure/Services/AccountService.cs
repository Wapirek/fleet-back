using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Specifications;

namespace Fleet.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountEntity> GetUserByNameAsync( string login )
        {
            var accSpec = new AccountSpecification ( login );
            var acc = await _unitOfWork.Repository<AccountEntity>().GetEntityWithSpecAsync ( accSpec );

            return acc;
        }

        public bool CheckPasswordAsync( AccountEntity account, string password )
        {
            return !VerifyPasswordHash ( password, account.Hash, account.PasswordSalt );
        }
        
        #region Private Methods
        
        /// <summary>
        /// Encrypt password inputed by user
        /// </summary>
        /// <param name="password">The password to encrypt</param>
        /// <param name="passwordHash">Create hash of the <see cref="password"/></param>
        /// <param name="passwordSalt">Create salt of the <see cref="password"/></param>
        private static void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Check inputed password with encrypt password
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <param name="passwordHash">The part of <see cref="password"/> hash</param>
        /// <param name="passwordSalt">The part of <see cref="password"/> salt</param>
        /// <returns>True if inputed password is matching with encrypt salt-hash parts, false otherwise</returns>
        private static bool VerifyPasswordHash(string password, IReadOnlyList<byte> passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            return !computeHash.Where ( ( hash, i ) => hash != passwordHash[i] ).Any();
        }
        
        #endregion
    }
}