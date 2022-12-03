using Fleet.Core.Entities;

namespace Fleet.Core.Specifications.Counts
{
    public class CashFlowCountSpec : BaseSpecification<CashFlowEntity>
    {
        public CashFlowCountSpec(int accountId) 
            : base (x => x.AccountId == accountId)
        {
        }
    }
}