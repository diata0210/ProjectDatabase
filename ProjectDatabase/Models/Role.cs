using System.ComponentModel.DataAnnotations;

namespace ProjectDatabase.Models
{
    public class Role
    {
        [Key]
        [MaxLength(8)]
        public string id { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string name { get; set; } = string.Empty;
        [StringLength(1000)]
        public string description { get; set; } = string.Empty;

        public ICollection<User> ? User { get; set; }
    }
}
