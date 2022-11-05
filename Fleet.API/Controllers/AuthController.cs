using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Dtos.Responser;
using Fleet.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    public class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        public AuthorizeTokenAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
    
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accService;
        private readonly ITokenService _tokenService;

        public AuthController(IAccountService accService,
            ITokenService tokenService)
        {
            _accService = accService;
            _tokenService = tokenService;
        }
        
        [HttpPost ( "login" )]
        public async Task<ActionResult<LoginResultDto>> LoginAsync ([FromBody] LoginDto loginDto)
        {
            var user = await _accService.GetUserByNameAsync ( loginDto.Login );
            if( user == null ) return Unauthorized ( new ApiResponse ( 401, "Nie poprawny login lub hasło" ) );

            if( _accService.CheckPasswordAsync ( user, loginDto.Password ) )
                return Unauthorized ( new ApiResponse ( 401, "Nie poprawny login lub hasło" ) );

            
            var response = new ApiResponse<LoginResultDto> ( 200, 
                "Zalogowano pomyślnie", 
                new LoginResultDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken ( user )
            });
            
            return Ok ( response );
        }

        
        [HttpPost ( "register" )]
        public async Task<ActionResult<LoginResultDto>> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var user = await _accService.GetUserByNameAsync ( registerDto.Login );
            if( user != null ) return Unauthorized ( new ApiResponse ( 404, "Nazwa użytkownika jest zarezerwowana" ) );

            var createdUser = await _accService.CreateUser ( registerDto );

            return Ok ( createdUser );
        }
    }
}