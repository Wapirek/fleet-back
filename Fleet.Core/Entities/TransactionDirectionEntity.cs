using System.ComponentModel.DataAnnotations.Schema;

namespace Fleet.Core.Entities
{
    [Table("kierunek_transakcji")]
    public class TransactionDirectionEntity : BaseEntity
    {
        /// <summary>
        /// Określa czy pieniądze wydaliśmy czy dostaliśmy
        /// </summary>
        [Column("kierunek")]
        public string TransactionDirection { get; set; }
    }
}