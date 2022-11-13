using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Interfaces.Services;
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

        [HttpPost ( "add-income" )]
        public async Task<ApiResponse<IncomeDto>> AddIncomeAsync( [FromBody] IncomeDto incomeDto )
        {
            return await _userProfileService.CreateIncomeAsync ( incomeDto );
        }
        
        [HttpPost ( "update-income" )]
        public async Task<ApiResponse<IncomeDto>> UpdateIncome( [FromBody] IncomeDto incomeDto )
        {
            return await _userProfileService.UpdateIncomeAsync ( incomeDto );
        }
        
        [HttpGet ( "get-income" )]
        public async Task<ApiResponse<IncomeDto>> GetIncome( [FromBody] IncomeLittleDto incomeDto )
        {
            var income = await _userProfileService.GetIncomeAsync ( incomeDto.Source, incomeDto.AccountId );
            var incomeToReturn = _map.Map<IncomeDto> ( income );
            return incomeToReturn == null
                    ? new ApiResponse<IncomeDto> ( 400, "Nie znaleziono przychodu", null )
                    : new ApiResponse<IncomeDto> ( 200, "", incomeToReturn )
                ;
        }
        
        [HttpGet ( "get-incomes" )]
        public async Task<ApiResponse<List<IncomeDto>>> GetIncomes( [FromHeader] int accountId )
        {
            var incomes= await _userProfileService.GetIncomesAsync ( accountId );
            return new ApiResponse<List<IncomeDto>> ( 200, "", incomes );
        }
        
        [HttpDelete ( "delete-income" )]
        public async Task<ApiResponse> GetIncomes( [FromBody] IncomeLittleDto incomeDto )
        {
            var income = await _userProfileService.DeleteIncome ( incomeDto );
            return income;
        }
    }
}