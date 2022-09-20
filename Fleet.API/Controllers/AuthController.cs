using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost ( "login" )]
        public async Task<ActionResult<LoginDto>> LoginAsync ([FromBody] LoginDto loginDto)
        {
            var response = new ApiResponse ( 200, "Zalogowano pomyślnie" );
            return Ok ( response );
        }
    }
}