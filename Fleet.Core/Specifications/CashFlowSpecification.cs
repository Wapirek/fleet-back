using Fleet.Core.Entities;

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
    }
}