using System;
using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class TransactionSpecification : BaseSpecification<TransactionEntity>
    {
        public TransactionSpecification( DateTime transactionDate, string transactionName, int accountId ) :
            base ( x => x.TransactionDate == transactionDate &&
                        x.TransactionName == transactionName &&
                        x.AccountId == accountId )
        {
            
        }
    }
}