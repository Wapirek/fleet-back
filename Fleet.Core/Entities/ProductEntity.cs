using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Fleet.Core.Entities
{
    [Table("produkty")]
    public class ProductEntity : BaseEntity
    {
        #region Columns

        [Column("nazwa_produktu")]
        [Required] [StringLength(100)]
        public string ProductName { get; set; }

        [Column("cena")]
        [Required]
        public double Price { get; set; }
        
        [Column("jednostka")]
        [Required]
        public string Unit { get; set; }

        #endregion

        #region Relations

        [AllowNull] 
        public int? CatalogId { get; set; }

        public CatalogEntity Catalog { get; set; }
        
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }

        public int productPlaceId { get; set; }
        public ProductPlaceEntity ProductPlace { get; set; }

        #endregion
    }
}