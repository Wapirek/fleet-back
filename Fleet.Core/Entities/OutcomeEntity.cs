using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleet.Core.Entities
{
    /// <summary>
    /// Stałe lub tymczasowe opłaty użytkownika
    /// </summary>
    [Table("opłaty_użytkownika")]
    public class OutcomeEntity : BaseEntity
    {
        #region Columns

        [Column("kwota_płatności")]
        public double Outcome { get; set; }

        [Column("Źródło")]
        public string Source { get; set; }
        
        /// <summary>
        /// Co ile dni wykonywana jest płatność
        /// </summary>
        [Column("cykliczność_dni")]
        public int PeriodicityDay { get; set; }

        /// <summary>
        /// Data kolejnego płatności
        /// </summary>
        [Column("kolejna_płatność")]
        public DateTime NextOutcome { get; set; }
        
        #endregion
        
        #region Relations
        
        public AccountEntity Account { get; set; }
        public int AccountId { get; set; }
        
        #endregion
    }
}