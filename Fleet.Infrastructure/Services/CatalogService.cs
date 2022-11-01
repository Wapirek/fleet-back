using System.Threading.Tasks;
using AutoMapper;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Specifications;

namespace Fleet.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public CatalogService(IUnitOfWork unitOfWork,
            IMapper map)
        {
            _unitOfWork = unitOfWork;
            _map = map;
        }
        
        /// <summary>
        /// Dodawanie katalogu
        /// </summary>
        /// <param name="catalogDto">Dane do utworzenia katalogu</param>
        /// <returns>Id utworzonego katalogu, lub zero jeśli się nie udało</returns>
        public async Task<int> AddCatalogAsync( CatalogDto catalogDto )
        {
            if( await IsExistCatalogAsync ( catalogDto ) ) return 0;
            var catalogToAdd = new CatalogEntity
            {
                AccountId = catalogDto.AccountId,
                CatalogName = catalogDto.CatalogName
            };

            _unitOfWork.Repository<CatalogEntity>().Add ( catalogToAdd );
            var id = await _unitOfWork.CompleteAsync();

            return id > 0 ? catalogToAdd.Id : 0;
        }

        public async Task<bool> IsExistCatalogAsync( CatalogDto catalogDto )
        {
            var catalogSpec = new CatalogSpecification ( catalogDto.CatalogName, catalogDto.AccountId );
            var catalogEntity = await _unitOfWork
                .Repository<CatalogEntity>()
                .GetEntityWithSpecAsync ( catalogSpec );

            return catalogEntity != null;
        }

        public async Task<CatalogDto> GetCatalogById( int catalogId )
        {
            var catalogEntity = await _unitOfWork.Repository<CatalogEntity>().GetByIdAsync ( catalogId );
            var catalogDto = _map.Map<CatalogDto> ( catalogEntity );

            return catalogDto;
        }
    }
}