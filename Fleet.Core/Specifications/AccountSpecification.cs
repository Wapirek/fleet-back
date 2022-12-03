using System.Reflection;
using Fleet.Core.Entities;
using Fleet.Core.Extensions;

namespace Fleet.Core.Specifications
{
    public class AccountSpecification : BaseSpecification<AccountEntity>
    {
        public AccountSpecification( string username, string email )
            : base ( x =>
                x.Username == username || x.Email == email
            )
        {
            
        }
    }
}