using System.ComponentModel.DataAnnotations.Schema;

namespace Fleet.Core.Entities
{
    [Table("profil_użytkownika")]
    public class UserProfileEntity : BaseEntity
    {
        #region Columns
        
        [Column("stan_konta")]
        public double AccountBalance { get; set; }
        
        #endregion
        
        #region Relations

        public AccountEntity Account { get; set; }
        public int AccountId { get; set; }
        
        #endregion
    }
}