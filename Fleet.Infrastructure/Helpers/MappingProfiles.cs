using AutoMapper;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;

namespace Fleet.Infrastructure.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CatalogForCatalogDto();
        }
        
        private void CatalogForCatalogDto()
        {
            CreateMap<CatalogEntity, CatalogDto>();
        }
    }
}