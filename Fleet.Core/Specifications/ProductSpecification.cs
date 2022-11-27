using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<ProductEntity>
    {
        public ProductSpecification(string productName, string catalog, string shop, int accountId)
            : base (x => x.AccountId == accountId &&
                         x.ProductName == productName && 
                         x.ProductPlace.Place == shop &&
                         x.Catalog.CatalogName == catalog)
        {
            AddInclude ( x => x.Catalog );
            AddInclude ( x => x.ProductPlace );
        }
        
        public ProductSpecification(string productName,  string shop, int accountId)
            : base (x => x.AccountId == accountId &&
                         x.ProductName == productName && 
                         x.ProductPlace.Place == shop)
        {
            AddInclude ( x => x.Catalog );
            AddInclude ( x => x.ProductPlace );
        }
    }
}