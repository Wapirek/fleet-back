using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [AuthorizeToken]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserProfileService _userProfileService;

        public TransactionController( ITransactionService transactionService,
            IUserProfileService userProfileService )
        {
            _transactionService = transactionService;
            _userProfileService = userProfileService;
        }

        [HttpPost ( "transaction-add" )]
        public async Task<ActionResult<ApiResponse<TransactionDto>>> AddTransaction([FromBody] TransactionDto transactionDto)
        {
            var result = await _transactionService.CreateTransaction ( transactionDto );
            var response = (TransactionDto) result.Response;
            if( response != null )
            {
                await _userProfileService.UpdateAccountBalanceAsync ( response.AccountId, response.TotalPaid,
                    response.TransactionDirectionId );

                return Ok ( new ApiResponse<TransactionDto> ( 200, "", response ) );
            }
            
            return BadRequest(new ApiResponse<TransactionDto> ( 400, result.Message, null ));
        }
    }
}