using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Helpers;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Specifications.Params;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [AuthorizeToken]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _map;

        public UserProfileController(IUserProfileService userProfileService,
            IMapper map)
        {
            _userProfileService = userProfileService;
            _map = map;
        }

        [HttpPost ( "add-cashflow" )]
        public async Task<ApiResponse<CashFlowDto>> AddIncomeAsync( [FromBody] CashFlowDto cashFlowDto )
        {
            return await _userProfileService.CreateCashFlowAsync ( cashFlowDto );
        }
        
        [HttpPost ( "update-cashflow" )]
        public async Task<ApiResponse<CashFlowDto>> UpdateIncome( [FromBody] CashFlowDto cashFlowDto )
        {
            return await _userProfileService.UpdateCashFlowAsync ( cashFlowDto );
        }
        
        [HttpGet ( "get-cashflow" )]
        public async Task<ApiResponse<CashFlowDto>> GetIncome( [FromBody] CashFlowLittleDto cashFlowDto )
        {
            var income = await _userProfileService.GetCashFlowAsync ( cashFlowDto.Source, cashFlowDto.AccountId );
            var incomeToReturn = _map.Map<CashFlowDto> ( income );
            return incomeToReturn == null
                    ? new ApiResponse<CashFlowDto> ( 400, "Nie znaleziono przychodu", null )
                    : new ApiResponse<CashFlowDto> ( 200, "", incomeToReturn )
                ;
        }
        
        [HttpGet ( "get-cashflows" )]
        public async Task<Pagination<CashFlowDto>> GetIncomes( [FromQuery] CashFlowSpecParams @params, int accId )
        {
            var incomes= await _userProfileService.GetCashFlowsAsync ( @params, accId );;
            return incomes;
        }
        
        [HttpDelete ( "delete-cashflow" )]
        public async Task<ApiResponse> GetIncomes( [FromBody] CashFlowLittleDto cashFlowDto )
        {
            var income = await _userProfileService.DeleteCashFlowAsync ( cashFlowDto );
            return income;
        }
    }
}