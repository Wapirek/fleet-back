using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Dtos.Responser;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Specifications;

namespace Fleet.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        #region Private Members
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        #endregion
        
        #region Constructors
        
        public AccountService(IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        
        #endregion
        
        #region Implemented Methods

        public async Task<AccountEntity> GetUserByNameAsync( string login )
        {
            var acc =  await _unitOfWork.Repository<AccountEntity>().GetByColumn ( "login", login );
            return acc;
        }

        public async Task<bool> IsExistEmail( string email )
        {
            var acc =  await _unitOfWork.Repository<AccountEntity>().GetByColumn ( "email", email );
            return acc != null;
        }
        

        public bool CheckPasswordAsync( AccountEntity account, string password )
        {
            return !VerifyPasswordHash ( password, account.Hash, account.PasswordSalt );
        }

        public async Task<ApiResponse<LoginResultDto>> CreateUser( RegisterDto registerDto )
        {
            #region Validate
            
            if( registerDto.Password.Length < 5 )
                return new ApiResponse<LoginResultDto> ( 404, "Wymagana ilość znaków na hasło: 5", null );
            
            if( string.IsNullOrEmpty ( registerDto.Login ) )
                return new ApiResponse<LoginResultDto> ( 404, "Nie uzupełniono nazwy użytkownika", null );

            MailAddress? mail;
            var isEmailCorrect = MailAddress.TryCreate( registerDto.Email, out mail );

            if( !isEmailCorrect )
                return new ApiResponse<LoginResultDto> ( 404, "Nie poprawny format maila", null );
            
            #endregion
            
            #region Creating
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashSalt( registerDto.Password, out passwordHash, out passwordSalt );

            var user = new AccountEntity
            {
                Username = registerDto.Login,
                Created = DateTime.Now,
                Email = registerDto.Email,
                Hash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _unitOfWork.Repository<AccountEntity>().Add ( user );
            var result = await _unitOfWork.CompleteAsync();

            #endregion

            #region Return Result
            
            if( result > 0 )
            {
                // powiąż konto do profilu użytkownika
                var userProfile = new UserProfileEntity
                {
                    AccountId = user.Id,
                    AccountBalance = 0
                };
                _unitOfWork.Repository<UserProfileEntity>().Add ( userProfile );
                await _unitOfWork.CompleteAsync();
                
                return new ApiResponse<LoginResultDto> ( 200, "", 
                    new LoginResultDto
                    {
                        Email = user.Email,
                        Token = await _tokenService.CreateToken ( user )
                    });
            }
            
            return new ApiResponse<LoginResultDto> ( 401, "Nie powiodło się tworzenie użytkownika", null );
            
            #endregion
        }

        #endregion
        
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