using System.ComponentModel.DataAnnotations;

namespace Fleet.Core.Entities
{
    public class BaseEntity
    {
        [Key] public int Id { get; set; }
    }
}