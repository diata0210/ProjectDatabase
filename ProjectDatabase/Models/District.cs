using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string name { get; set; } = string.Empty;
        [Required]
        public int province_id { get; set; }
        public Province ? Province { get; set; }
        public ICollection<Store> ? Stores { get; set; }

    }
}
