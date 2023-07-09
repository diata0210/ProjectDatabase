using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Thiết lập tự động tăng cho ID

        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string ? name { get; set; }
        [MaxLength(1000)]
        public string description { get; set; } = string.Empty;

        public ICollection<User> ? Users { get; set; }
    }
}
