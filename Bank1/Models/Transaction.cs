using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank1.Models
{
    [Table("tbTransaction")]
    public class Transaction
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("TransDate")]
        [StringLength(50)]
        public string TransDate { get; set; } = string.Empty;

        [Column("Withdraw", TypeName = "decimal(15,2)")]
        public decimal Withdraw { get; set; }

        [Column("Deposit", TypeName = "decimal(15,2)")]
        public decimal Deposit { get; set; }

        [Column("Balance", TypeName = "decimal(15,2)")]
        public decimal Balance { get; set; }

        [Column("AccountName")]
        [StringLength(20)]
        public string AccountName { get; set; } = string.Empty;
    }
}
