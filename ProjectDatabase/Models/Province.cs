using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Thiết lập tự động tăng cho ID

        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string name { get; set; } = string.Empty;
        public ICollection<District> ? Districts { get; set; }
        public ICollection<Store> ? Stores { get; set; }

    }
}
