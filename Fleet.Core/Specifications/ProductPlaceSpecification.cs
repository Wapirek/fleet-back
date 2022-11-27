using Fleet.Core.Entities;

namespace Fleet.Core.Specifications
{
    public class ProductPlaceSpecification : BaseSpecification<ProductPlaceEntity>
    {
        public ProductPlaceSpecification( string place ) :
            base ( x => x.Place == place )
        {

        }
    }
}