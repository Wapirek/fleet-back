using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Specifications;

namespace Fleet.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ProductEntity> GetProduct( string product, string shop, int accountId )
        {
            var productSpec = new ProductSpecification ( product, shop, accountId );
            var productEntity = await _unitOfWork.Repository<ProductEntity>().GetEntityWithSpecAsync ( productSpec );
            return productEntity;
        }
        
        public async Task<ProductEntity> GetProduct( string product, int placeId, int accountId )
        {
            var productSpec = new ProductSpecification ( product, placeId, accountId );
            var productEntity = await _unitOfWork.Repository<ProductEntity>().GetEntityWithSpecAsync ( productSpec );
            return productEntity;
        }

        public async Task<ProductEntity> CreateProduct( ProductEntity product )
        {
            _unitOfWork.Repository<ProductEntity>().Add ( product );
            await _unitOfWork.CompleteAsync();
            return product;
        }
    }
}