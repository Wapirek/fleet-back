using System.Collections.Generic;
using Fleet.Core.Entities;

namespace Fleet.Infrastructure.Data
{
    public class DataModel
    {
        public AccountEntity Account { get; set; }
        public List<CatalogEntity> Catalogs { get; set; }
        public List<ProductEntity> Products { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }
}