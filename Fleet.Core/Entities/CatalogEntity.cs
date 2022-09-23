using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleet.Core.Entities
{
    /// <summary>
    /// Katalog zawierający produkty
    /// </summary>
    [Table("katalog_produktów")]
    public class CatalogEntity : BaseEntity
    {
        #region Columns

        [Column("nazwa")]
        [Required] [StringLength(60)]
        public string CatalogName { get; set; }
        
        #endregion
        
        #region Relations
        
        public ICollection<ProductEntity> Produts { get; set; }

        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }

        #endregion
    }
}