﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;
using Fleet.Core.Helpers;
using Fleet.Core.Specifications.Params;

namespace Fleet.Core.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<ApiResponse<CashFlowDto>> CreateCashFlowAsync( CashFlowDto cashFlow );
        Task<ApiResponse<CashFlowDto>> UpdateCashFlowAsync( CashFlowDto cashFlow );
        Task<ApiResponse> DeleteCashFlowAsync( CashFlowLittleDto cashFlow );
        
        /// <summary>
        /// Aktualizuje stan konta użytkownika na podstawie stałych wpływów i opłat
        /// </summary>
        Task UpdateAccountBalanceAsync();

        Task UpdateAccountBalanceAsync( int accountId, double paid, int transactionDirectionId );

        Task<CashFlowEntity> GetCashFlowAsync( string source, int accountId );

        Task<Pagination<CashFlowDto>> GetCashFlowsAsync( CashFlowSpecParams @params, int accountId );
    }
}