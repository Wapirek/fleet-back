using System.Threading.Tasks;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductEntity> GetProduct( string product, string shop, int accountId );
        Task<ProductEntity> GetProduct( string product, int placeId, int accountId );
        Task<ProductEntity> CreateProduct( ProductEntity product );
    }
}