using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Fleet.Core.Entities
{
    [Table("transakcja_pozycje")]
    public class TransactionPostionsEntity : BaseEntity
    {
        #region Columns

        [Column("ilość")]
        [Required]
        public double Quantity { get; set; }

        [Column("zapłacono")]
        public double Paid { get; set; }
        
        #endregion
        
        #region Relations
        
        [AllowNull]
        public int? ProductId { get; set; }

        public List<ProductEntity> Product { get; set; }

        public int TransactionId { get; set; }
        public TransactionEntity Transaction { get; set; }
        
        #endregion
    }
}