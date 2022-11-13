using System.Collections.Generic;
using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<ApiResponse<IncomeDto>> CreateIncomeAsync( IncomeDto income );
        Task<ApiResponse<IncomeDto>> UpdateIncomeAsync( IncomeDto income );
        Task<ApiResponse> DeleteIncome( IncomeLittleDto income );
        
        /// <summary>
        /// Aktualizuje stan konta użytkownika na podstawie stałych wpływów i opłat
        /// </summary>
        Task UpdateAccountBalanceAsync();

        Task<IncomeEntity> GetIncomeAsync( string source, int accountId );

        Task<IReadOnlyList<IncomeDto>> GetIncomesAsync( int accountId );
    }
}