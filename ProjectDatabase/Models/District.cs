using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class District
    {
        [Key]
        [MaxLength(8)]
        public string id { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string name { get; set; } = string.Empty;
        [ForeignKey("Province")]
        [MaxLength(8)]
        [Required]
        public string ? province_id { get; set; }
        public Province ? Province { get; set; }
        public ICollection<Store> ? Store { get; set; }

    }
}
