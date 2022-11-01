using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class CatalogSpecification : BaseSpecification<CatalogEntity>
    {
        public CatalogSpecification(string name, int userId) 
            : base (x => x.CatalogName == name &&
                         x.AccountId == userId)
        {
            
        }
    }
}