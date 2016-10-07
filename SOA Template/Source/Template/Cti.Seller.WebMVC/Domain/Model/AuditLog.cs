using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecrm.Domain.Model
{
    public class AuditLog : BaseDomainModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditLogID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int RecordType { get; set; }
        [Required]
        public int TransactionType { get; set; }
        [Required]
        public int RecordID { get; set; }
        [Required]
        public string Data { get; set; }
    }
}