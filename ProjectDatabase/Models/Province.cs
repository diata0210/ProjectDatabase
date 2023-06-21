using System.ComponentModel.DataAnnotations;

namespace ProjectDatabase.Models
{
    public class Province
    {
        [Key]
        [MaxLength(8)]
        public string id { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string name { get; set; } = string.Empty;
        public ICollection<District> ? District { get; set; }
        public ICollection<Store> ? Store { get; set; }

    }
}
