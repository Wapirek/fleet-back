using System.Threading.Tasks;
using Fleet.Core.Dtos;

namespace Fleet.Core.Interfaces.Services
{
    public interface ICatalogService
    {
        Task<int> AddCatalogAsync( CatalogDto catalogDto );
        Task<bool> IsExistCatalogAsync( CatalogDto catalogDto );
        Task<CatalogDto> GetCatalogById( int catalogId );
    }
}