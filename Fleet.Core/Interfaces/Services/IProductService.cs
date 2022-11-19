using System.Threading.Tasks;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductEntity> GetProduct( int id );
    }
}