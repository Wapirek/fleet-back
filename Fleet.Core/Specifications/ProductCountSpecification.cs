using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class ProductCountSpecification : BaseSpecification<ProductEntity>
    {
        public ProductCountSpecification(string productName, string catalog, string shop, int accountId)
            : base (x => x.AccountId == accountId &&
                         x.ProductName == productName && 
                         x.ProductPlace.Place == shop &&
                         x.Catalog.CatalogName != catalog)
        {
            AddInclude ( x => x.Catalog );
            AddInclude ( x => x.ProductPlace );
        }
    }
}