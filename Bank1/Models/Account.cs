using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank1.Models
{
    [Table("tbAccount")]
    public class Account
    {
        [Key]
        [Column("AccountName")]
        [StringLength(20)]
        public string AccountName { get; set; } = string.Empty;

        [Column("Pincode")]
        public int Pincode { get; set; }
    }
}
