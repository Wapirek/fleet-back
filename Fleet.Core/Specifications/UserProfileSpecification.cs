using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class UserProfileSpecification : BaseSpecification<IncomeEntity>
    {
        public UserProfileSpecification(string source, int accountId) 
            : base (x => x.Source == source &&
                         x.AccountId == accountId)
        {
            AddInclude ( x => x.Account );
        }
        
        public UserProfileSpecification(int accountId) 
            : base (x => x.AccountId == accountId)
        {
            AddInclude ( x => x.Account );
        }
    }
}