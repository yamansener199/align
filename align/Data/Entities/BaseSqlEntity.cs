using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace align.Data.Entities
{
    public abstract class BaseSqlEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ChangedAt { get; set; }
        public string? ChangedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
