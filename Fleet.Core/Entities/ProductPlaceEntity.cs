using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleet.Core.Entities
{
    [Table("produkty_źródło")]
    public class ProductPlaceEntity : BaseEntity
    {
        #region Columns
        
        [Column("place")]
        [Required][StringLength(100)]
        public string Place { get; set; }
        
        #endregion
        
        #region Relations

        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }
        
        #endregion
    }
}