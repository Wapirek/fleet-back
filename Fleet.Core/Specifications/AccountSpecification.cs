using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class AccountSpecification : BaseSpecification<AccountEntity>
    {
        public AccountSpecification(string login)
            : base (x => x.Username == login)
        {
            
        }
    }
}