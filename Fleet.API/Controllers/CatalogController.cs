using System.Threading.Tasks;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [AuthorizeToken]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpPost ( "add" )]
        public async Task<ActionResult<ApiResponse<CatalogDto>>> CreateCatalogAsync( [FromBody] CatalogDto catalogDto )
        {
            var result = await _catalogService.AddCatalogAsync ( catalogDto );
            if( result == 0 ) return BadRequest ( new ApiResponse ( 400, "Nie udało się utworzyć katalogu" ) );

            var catalogToReturn = await _catalogService.GetCatalogById ( result );
            return Ok ( new ApiResponse<CatalogDto> ( 200, "", catalogToReturn ) );
        }
    }
}