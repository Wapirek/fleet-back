using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleet.Core.Entities
{
    [Table("konto")]
    public class AccountEntity : BaseEntity
    {
        [Column("login")]
        [Required] [StringLength(30)]
        public string Username { get; set; }

        [Column("email")]
        [Required] [StringLength(200)]
        public string Email { get; set; }

        [Column("salt")]
        public byte[] PasswordSalt { get; set; }
        
        [Column("hash")] 
        public byte[] Hash { get; set; }

        [Column("utworzono")]
        public DateTime Created { get; set; }
    }
}