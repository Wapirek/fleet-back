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
            IncomeDtoForIncome();
            IncomeForIncomeDto();
        }
        
        private void CatalogForCatalogDto()
        {
            CreateMap<CatalogEntity, CatalogDto>();
        }

        private void IncomeDtoForIncome()
        {
            CreateMap<CashFlowEntity, CashFlowDto>().ReverseMap();
        }
        
        private void IncomeForIncomeDto()
        {
            CreateMap<CashFlowEntity, CashFlowDto>();
        }
    }
}