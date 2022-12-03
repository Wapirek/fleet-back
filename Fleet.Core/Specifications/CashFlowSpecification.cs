using Fleet.Core.Entities;
using Fleet.Core.Specifications.Params;

namespace Fleet.Core.Specifications
{
    public class CashFlowSpecification : BaseSpecification<CashFlowEntity>
    {
        public CashFlowSpecification(string source, int accountId) 
            : base (x => x.Source == source &&
                         x.AccountId == accountId)
        {
            AddInclude ( x => x.Account );
        }
        
        public CashFlowSpecification(int accountId) 
            : base (x => x.AccountId == accountId)
        {
            AddInclude ( x => x.Account );
        }

        public CashFlowSpecification(CashFlowSpecParams @params, int accountId)
        : base (x => x.AccountId == accountId)
        {
            ApplyPaging ( @params.PageSize * ( @params.PageIndex - 1 ), @params.PageSize );
        }
    }
}