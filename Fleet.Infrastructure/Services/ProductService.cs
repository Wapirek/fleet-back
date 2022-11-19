using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;

namespace Fleet.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ProductEntity> GetProduct( int id )
        {
            return await _unitOfWork.Repository<ProductEntity>().GetByIdAsync ( id );
        }
    }
}