using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fleet.Core.Enums;

namespace Fleet.Core.Entities
{
    [Table("przepływy_pieniężne")]
    public class CashFlowEntity : BaseEntity
    {
        #region Columns
        
        [Column("obciążenie")]
        [Required]
        public double Charge { get; set; }

        [Column("Źródło")]
        [Required]
        public string Source { get; set; }

        /// <summary>
        /// Co ile dni wykonywany jest przepływ środków pieniężnych na konto
        /// </summary>
        [Column("cykliczność_dni")]
        [Required]
        public int PeriodicityDay { get; set; }

        /// <summary>
        /// Data kolejnego przepływu danego środku pieniężnego
        /// Uwaga: Pole w trybie do edycji z brakiem możliwości edycji na dzień bieżący.
        /// </summary>
        [Column("kolejny_przepływ")]
        [Required]
        public DateTime NextCashFlow { get; set; }

        [Column("rodzaj_przepływu")]
        [Required]
        public string CashFlowKind { get; set; }
        
        #endregion
        
        #region Relations
        
        public AccountEntity Account { get; set; }
        [Required]
        public int AccountId { get; set; }
        
        #endregion
    }
}